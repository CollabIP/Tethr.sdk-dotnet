using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Authentication;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Tethr.Sdk.Session;

/// <summary>
/// Manages the connection to the Tethr server and authentication session.
/// </summary>
/// <remarks>
/// Exposed the Raw action for calling the Tethr server, and maintains the authentication token.
/// This object is thread safe, and should be reused and often is a singleton kept for the lifecycle of the application.
/// </remarks>
public class TethrSession : ITethrSession, IDisposable
{
    private readonly ILogger<TethrSession> _log;
    private static ProductInfoHeaderValue? _productInfoHeaderValue;
    private readonly object _authLock = new();
    private string _apiUser;
    private SecureString _apiPassword;
    private HttpClient _client;
    private TokenResponse? _apiToken;
    private readonly IDisposable? _optionsMonitor;

    public TethrSession(IOptionsMonitor<TethrOptions> options, ILogger<TethrSession>? logger = null)
    {
        _log = logger ?? NullLogger<TethrSession>.Instance;

        if (string.IsNullOrEmpty(options.CurrentValue.Uri))
            throw new ArgumentNullException(nameof(options.CurrentValue.Uri));
        if (string.IsNullOrEmpty(options.CurrentValue.ApiUser))
            throw new ArgumentNullException(nameof(options.CurrentValue.ApiUser));
        if (string.IsNullOrEmpty(options.CurrentValue.Password))
            throw new ArgumentNullException(nameof(options.CurrentValue.Password));

        var hostUri = new Uri(options.CurrentValue.Uri, UriKind.Absolute);

        if (!hostUri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
        {
            _log.InsecureConnectionWarning();
        }

        _apiUser = options.CurrentValue.ApiUser;
        _apiPassword = ToSecureString(options.CurrentValue.Password);
        _client = CreateHttpClient(hostUri);

        _optionsMonitor = options.OnChange((o, _) =>
        {
            lock (_authLock)
            {
                try
                {
                    var valid = true;
                    if (string.IsNullOrEmpty(o.Uri))
                    {
                        _log.ErrorUpdatingOptionsMissingOption("Uri");
                        valid = false;
                    }

                    if (string.IsNullOrEmpty(o.ApiUser))
                    {
                        _log.ErrorUpdatingOptionsMissingOption("ApiUser");
                        valid = false;
                    }

                    if (string.IsNullOrEmpty(o.Password))
                    {
                        _log.ErrorUpdatingOptionsMissingOption("Password");
                        valid = false;
                    }

                    if (!valid) return;

                    var newHostUri = new Uri(o.Uri!, UriKind.Absolute);
                    if (newHostUri != _client.BaseAddress)
                    {
                        // NOTE: Not disposing the old client, It's a bad idea to dispose of a HttpClient, and it's not necessary to do so.
                        _client = CreateHttpClient(newHostUri);
                    }

                    _apiUser = o.ApiUser!;
                    _apiPassword = ToSecureString(o.Password!);

                    ClearAuthToken();
                }
                catch (Exception e)
                {
                    _log.ErrorUpdatingOptions(e);
                }
            }
        });
    }

    /// <summary>
    /// When True, if Tethr returns a 401 (Unauthorized), 
    /// automatically reset the OAuth Token and request a new one with the next request
    /// </summary>
    /// <remarks>
    /// Default value is True.
    /// 
    /// Typically the only cause for a 401 is if the Token has been revoked.
    /// By having this true, it would allow a client to retry the request and probably
    /// be successful.
    /// 
    /// If you want more control over how the reset is handled, you can set this to false.
    /// </remarks>
    public bool ResetAuthTokenOnUnauthorized { get; set; } = true;

    public static IWebProxy? DefaultProxy { get; set; } = WebRequest.DefaultWebProxy;

    /// <summary>
    /// Add data used to in the HTTP User-Agent Header for requests to Tethr.
    /// </summary>
    /// <param name="product">The name of the product</param>
    /// <param name="version">The version number of the product</param>
    public static void SetProductInfoHeaderValue(string product, string version)
    {
        _productInfoHeaderValue = new ProductInfoHeaderValue(product, version);
    }

    /// <inheritdoc/>
    public void ClearAuthToken()
    {
        lock (_authLock)
        {
            _apiToken = null;
        }
    }

    /// <inheritdoc/>
    public async Task<T> GetAsync<T>(
        string resourcePath,
        JsonTypeInfo<T> outputJsonInfo,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, CreateUri(resourcePath));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", GetApiAuthToken(cancellationToken));
        using var message = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        EnsureAuthorizedStatusCode(message);

        if (message.StatusCode == HttpStatusCode.NotFound)
            throw new KeyNotFoundException("The requested resource was not found in Tethr");

        message.EnsureSuccessStatusCode();
        if (message.Content.Headers.ContentType?.MediaType?.Equals("application/json",
                StringComparison.OrdinalIgnoreCase) != true)
        {
            throw new InvalidOperationException(
                $"Unexpected content type ({message.Content.Headers.ContentType}) returned from server.");
        }

        await using var s = await message.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        return JsonSerializer.Deserialize(s, outputJsonInfo) ??
               throw new TethrSessionProcessingException("Failed to deserialize response");
    }

    /// <inheritdoc/>
    public async Task<Stream> GetStreamAsync(
        string resourcePath,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, CreateUri(resourcePath));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", GetApiAuthToken(cancellationToken));
        var message = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        try
        {
            EnsureAuthorizedStatusCode(message);

            if (message.StatusCode == HttpStatusCode.NotFound)
                throw new KeyNotFoundException("The requested resource was not found in Tethr");

            message.EnsureSuccessStatusCode();
            if (message.Content.Headers.ContentType?.MediaType?.Equals("audio/mpeg",
                    StringComparison.OrdinalIgnoreCase) != true)
            {
                throw new InvalidOperationException(
                    $"Unexpected content type ({message.Content.Headers.ContentType}) returned from server.");
            }

            return await message.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        }
        catch 
        {
            // Make sure message is disposed if we have an exception, as the calling code can't dispose the stream.
            message.Dispose();
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<TOut> PostMultiPartAsync<TIn, TOut>(
        string resourcePath,
        TIn info,
        JsonTypeInfo<TIn> inputJsonInfo,
        Stream buffer,
        JsonTypeInfo<TOut> outputJsonInfo,
        CancellationToken cancellationToken,
        string dataPartMediaType = "application/octet-stream")
    {
        try
        {
            using var content = new MultipartFormDataContent(Guid.NewGuid().ToString());

            // Create a stream for the JSON part
            var jsonPartStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(jsonPartStream, info, inputJsonInfo, cancellationToken)
                .ConfigureAwait(false);
            jsonPartStream.Seek(0, SeekOrigin.Begin);
            var infoContent = new StreamContent(jsonPartStream);
            infoContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Handle the binary data part
            var streamContent = new StreamContent(buffer);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(dataPartMediaType);

            content.Add(infoContent, "info");
            content.Add(streamContent, "data");

            using var request = new HttpRequestMessage(HttpMethod.Post, CreateUri(resourcePath));
            request.Content = content;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", GetApiAuthToken(cancellationToken));
            using var message = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);

            EnsureAuthorizedStatusCode(message);
            message.EnsureSuccessStatusCode();

            if (message.Content.Headers.ContentType?.MediaType?.Equals("application/json",
                    StringComparison.OrdinalIgnoreCase) != true)
            {
                throw new TethrSessionProcessingException(
                    $"Unexpected content type ({message.Content.Headers.ContentType}) returned from server.");
            }

            await using var s = await message.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
            return JsonSerializer.Deserialize(s, outputJsonInfo) ??
                   throw new TethrSessionProcessingException("Failed to deserialize response");
        }
        catch (Exception e) when (e is ApplicationException or JsonException or ArgumentException
                                      or InvalidOperationException or NotSupportedException)
        {
            System.Diagnostics.Debug.Assert(false,
                "Exception thrown from tethrSession.PostMultiPartAsync, that should have a guard clause in place.");
            throw new TethrSessionProcessingException(e);
        }
    }

    /// <inheritdoc/>
    public async Task<TOut> PostAsync<TIn, TOut>(
        string resourcePath,
        TIn body,
        JsonTypeInfo<TIn> inputJsonInfo,
        JsonTypeInfo<TOut> outputJsonInfo,
        CancellationToken cancellationToken)
    {
        var requestContentStream = new MemoryStream();
        await JsonSerializer.SerializeAsync(requestContentStream, body, inputJsonInfo, cancellationToken: default)
            .ConfigureAwait(false);
        requestContentStream.Seek(0, SeekOrigin.Begin);

        using var content = new StreamContent(requestContentStream);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        using var request = new HttpRequestMessage(HttpMethod.Post, CreateUri(resourcePath));
        request.Content = content;
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", GetApiAuthToken(cancellationToken));
        using var message = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        EnsureAuthorizedStatusCode(message);
        message.EnsureSuccessStatusCode();

        if (message.Content.Headers.ContentType?.MediaType?
                .Equals("application/json", StringComparison.OrdinalIgnoreCase) != true)
            throw new TethrSessionProcessingException(
                $"Unexpected content type ({message.Content.Headers.ContentType}) returned from server.");

        await using var s = await message.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        return JsonSerializer.Deserialize(s, outputJsonInfo) ??
               throw new TethrSessionProcessingException("Failed to deserialize response");
    }

    /// <inheritdoc/>
    public async Task PostAsync<TIn>(
        string resourcePath,
        TIn body,
        JsonTypeInfo<TIn> inputJsonInfo,
        CancellationToken cancellationToken)
    {
        using var requestContentStream = new MemoryStream();
        await JsonSerializer.SerializeAsync(requestContentStream, body, inputJsonInfo, cancellationToken)
            .ConfigureAwait(false);
        requestContentStream.Seek(0, SeekOrigin.Begin);

        using var content = new StreamContent(requestContentStream);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        using var request = new HttpRequestMessage(HttpMethod.Post, CreateUri(resourcePath));
        request.Content = content;
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", GetApiAuthToken(cancellationToken));
        using var message = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        EnsureAuthorizedStatusCode(message);
        message.EnsureSuccessStatusCode();
    }

    /// <inheritdoc/>
    public async Task PostAsync(
        string resourcePath,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, CreateUri(resourcePath));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", GetApiAuthToken(cancellationToken));
        using var message = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        EnsureAuthorizedStatusCode(message);
        message.EnsureSuccessStatusCode();
    }

    private Uri CreateUri(string uri)
    {
        if (string.IsNullOrEmpty(uri))
            throw new ArgumentNullException(nameof(uri));

        var u = new Uri(uri, UriKind.Relative);
        _log.MakingRequest(u);
        return u;
    }

    private string GetApiAuthToken(CancellationToken cancellationToken, bool force = false)
    {
        // ReSharper disable once InvertIf
        if (force || _apiToken?.IsValid != true || string.IsNullOrEmpty(_apiToken.AccessToken))
        {
            lock (_authLock)
            {
                // ReSharper disable once InvertIf
                if (force || _apiToken?.IsValid != true)
                {
                    // Would like to have this called via Async, but because we are in a lock
                    // we would need to pull in another 3rd party library.
                    var t = GetClientCredentialsAsync(_apiUser, _apiPassword, cancellationToken).ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();

                    if (t.IsValid == false || string.IsNullOrEmpty(t.AccessToken))
                        throw new AuthenticationException("Failed to get a valid token from Tethr");

                    _apiToken = t;
                }
            }
        }

        return _apiToken.AccessToken ?? throw new AuthenticationException("No valid token available");
    }

    private void EnsureAuthorizedStatusCode(HttpResponseMessage message)
    {
        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault, We only care about Unauthorized and Forbidden
        switch (message.StatusCode)
        {
            case HttpStatusCode.Unauthorized:
            {
                if (ResetAuthTokenOnUnauthorized)
                    _apiToken = null;
                throw new AuthenticationException("Request returned 401 (Unauthorized)");
            }
            case HttpStatusCode.Forbidden:
                throw new UnauthorizedAccessException("Request returned 403 (Forbidden)");
        }
    }

    private async Task<TokenResponse> GetClientCredentialsAsync(
        string clientId,
        SecureString clientSecret,
        CancellationToken cancellationToken)
    {
        _log.MakingRequest(_client.BaseAddress);

        using HttpContent r = new FormUrlEncodedContent(
            new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                {
                    "client_secret",
                    ToUnsecureString(clientSecret) ?? throw new ArgumentNullException(nameof(clientSecret))
                },
                { "client_id", clientId }
            });
        using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, CreateUri("/Token"));
        httpRequestMessage.Content = r;
        using var response = await _client
            .SendAsync(httpRequestMessage, cancellationToken)
            .ConfigureAwait(false);

        if (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
            throw new UnauthorizedAccessException(
                $"Server returned {response.StatusCode} to request to get Access Token.");

        response.EnsureSuccessStatusCode();
        
        // Parse the response
        var json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        var t = ParseTokenResponse(json);

        _log.TokenReceived(t.TokenType ?? "Unknown", t.ExpiresInSeconds);
        if (t.TokenType != "bearer")
        {
            throw new InvalidOperationException("Can only support Bearer tokens");
        }

        return t;
    }

    /// <summary>
    /// Parse a TokenResponse from a JSON string
    /// </summary>
    /// <param name="json">The JSON response from an OAuth toke request</param>
    /// <returns>A TokenResponse object</returns>
    /// <exception cref="InvalidOperationException"></exception>
    private TokenResponse ParseTokenResponse(string json)
    {
        // We are manually parsing the JSON here, as the System.Text.Json source generator has a bug where it is ignoring the JsonPropertyName attribute
        // For .Net 8 we could use JsonNamingPolicy.SnakeCaseLower, but that is not available in .Net 6
        var tokenJsonObject = JsonNode.Parse(json);
        if (tokenJsonObject == null)
            throw new InvalidOperationException("Failed to parse token response");

        var t = new TokenResponse
        {
            AccessToken = tokenJsonObject["access_token"]?.ToString(),
            TokenType = tokenJsonObject["token_type"]?.ToString()
        };

        if (long.TryParse(tokenJsonObject["expires_in"]?.ToString(), out var expiresInSeconds))
        {
            t.ExpiresInSeconds = expiresInSeconds;
        }

        return t;
    }

    private static string? ToUnsecureString(SecureString securePassword)
    {
        ArgumentNullException.ThrowIfNull(securePassword);

        var unmanagedString = IntPtr.Zero;
        try
        {
            unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
            return Marshal.PtrToStringUni(unmanagedString);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
        }
    }

    private static SecureString ToSecureString(string text)
    {
        var secure = new SecureString();
        foreach (var c in text)
        {
            secure.AppendChar(c);
        }

        return secure;
    }

    private HttpClient CreateHttpClient(Uri hostUri)
    {
        var version = typeof(TethrSession).Assembly.GetName().Version?.ToString() ?? "Unknown";
        var proxyUri = string.Empty;
        var httpHandler = new HttpClientHandler { UseCookies = false, AllowAutoRedirect = false };
        var proxy = DefaultProxy;

        try
        {
            proxyUri = proxy?.GetProxy(hostUri)?.ToString() ?? "None";
        }
        catch (PlatformNotSupportedException ex)
        {
            _log.ProxyError(ex);
        }

        _log.RequestInitiated(hostUri.ToString(), version, proxyUri);

        httpHandler.Proxy = proxy;
        httpHandler.UseProxy = true;

        var client = new HttpClient(httpHandler, true)
        {
            BaseAddress = hostUri,
            Timeout = TimeSpan.FromMinutes(5),
            DefaultRequestHeaders =
            {
                UserAgent =
                {
                    new ProductInfoHeaderValue("TethrAudioBroker", version),
                    new ProductInfoHeaderValue($"({Environment.OSVersion})"),
                    new ProductInfoHeaderValue("DotNet-CLR", Environment.Version.ToString())
                }
            }
        };

        if (_productInfoHeaderValue != null)
            client.DefaultRequestHeaders.UserAgent.Add(_productInfoHeaderValue);

        return client;
    }

    public void Dispose()
    {
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract, It shouldn't be null, but if it is, we don't want to throw an exception
        _apiPassword?.Dispose();
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract, It shouldn't be null, but if it is, we don't want to throw an exception
        _client?.Dispose();
        _optionsMonitor?.Dispose();
    }
}