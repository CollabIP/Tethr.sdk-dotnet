using System.Text.Json.Serialization.Metadata;

namespace Tethr.Sdk.Session;

/// <summary>
/// Manages the connection to the Tethr server and authentication session.
/// </summary>
/// <remarks>
/// Exposed the Raw action for calling the Tethr server, and maintains the authentication token.
/// This object is thread safe, and should be reused and often is a singleton kept for the lifecycle of the application.
/// </remarks>
public interface ITethrSession
{
    /// <summary>
    /// Clears the current authentication token.
    /// </summary>
    /// <remarks>
    /// This will force the a new Authentication token to be retrieved on the next request.
    /// Often called on the fist <see cref="System.Security.Authentication.AuthenticationException"/>, as it's probable that the token was simply revoked
    /// </remarks>
    void ClearAuthToken();

    /// <summary>
    /// Make a GET request to the server, and return the result
    /// </summary>
    /// <typeparam name="TOut">The object to fill with the result from the server.</typeparam>
    /// <param name="resourcePath">The path to the resource to get.</param>
    /// <param name="outputJsonInfo">The Json Info from JsonSerializerContext for the type that should be returned.</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>TOut filled with the result from the server.</returns>
    /// <exception cref="T:Tethr.Sdk.TethrSessionProcessingException">Thrown when a critical error occurs within the Tethr SDK, indicating a failure in session processing. This exception highlights severe issues that disrupt normal operation.</exception>
    /// <exception cref="T:System.Net.Http.HttpRequestException">The HTTP response is unsuccessful.</exception>
    /// <exception cref="T:System.Security.Authentication.AuthenticationException">Request returned 401 (Unauthorized)</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">Request returned 403 (Forbidden)</exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The request failed due to timeout.</exception>
    Task<TOut> GetAsync<TOut>(string resourcePath,
        JsonTypeInfo<TOut> outputJsonInfo,
        CancellationToken cancellationToken);

    /// <summary>
    /// Make a POST request to the server, and return the result
    /// </summary>
    /// <typeparam name="TIn">The object type of the body.</typeparam>
    /// <typeparam name="TOut">The object to fill with the result from the server.</typeparam>
    /// <param name="resourcePath">The path to the resource.</param>
    /// <param name="body">The request body to be sent to the server as JSON.</param>
    /// <param name="inputJsonInfo">The Json Info from JsonSerializerContext for the body to send.</param>
    /// <param name="outputJsonInfo">The Json Info from JsonSerializerContext for the type that should be returned.</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>TOut filled with the result from the server.</returns>
    /// <exception cref="T:Tethr.Sdk.TethrSessionProcessingException">Thrown when a critical error occurs within the Tethr SDK, indicating a failure in session processing. This exception highlights severe issues that disrupt normal operation.</exception>
    /// <exception cref="T:System.Net.Http.HttpRequestException">The HTTP response is unsuccessful.</exception>
    /// <exception cref="T:System.Security.Authentication.AuthenticationException">Request returned 401 (Unauthorized)</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">Request returned 403 (Forbidden)</exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The request failed due to timeout.</exception>
    Task<TOut> PostAsync<TIn, TOut>(string resourcePath,
        TIn body,
        JsonTypeInfo<TIn> inputJsonInfo,
        JsonTypeInfo<TOut> outputJsonInfo,
        CancellationToken cancellationToken);

    /// <summary>
    /// Make a POST request to the server that doesn't return data.
    /// </summary>
    /// <typeparam name="TIn">The object type of the body.</typeparam>
    /// <param name="resourcePath">The path to the resource.</param>
    /// <param name="body">The request body to be sent to the server as JSON.</param>
    /// <param name="inputJsonInfo">The Json Info from JsonSerializerContext for the body to send.</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <exception cref="T:Tethr.Sdk.TethrSessionProcessingException">Thrown when a critical error occurs within the Tethr SDK, indicating a failure in session processing. This exception highlights severe issues that disrupt normal operation.</exception>
    /// <exception cref="T:System.Net.Http.HttpRequestException">The HTTP response is unsuccessful.</exception>
    /// <exception cref="T:System.Security.Authentication.AuthenticationException">Request returned 401 (Unauthorized)</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">Request returned 403 (Forbidden)</exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The request failed due to timeout.</exception>
    Task PostAsync<TIn>(string resourcePath,
        TIn body,
        JsonTypeInfo<TIn> inputJsonInfo,
        CancellationToken cancellationToken);

    /// <summary>
    /// Make a POST request to the server that doesn't send or return data.
    /// </summary>
    /// <param name="resourcePath">The path to the resource.</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <exception cref="T:Tethr.Sdk.TethrSessionProcessingException">Thrown when a critical error occurs within the Tethr SDK, indicating a failure in session processing. This exception highlights severe issues that disrupt normal operation.</exception>
    /// <exception cref="T:System.Net.Http.HttpRequestException">The HTTP response is unsuccessful.</exception>
    /// <exception cref="T:System.Security.Authentication.AuthenticationException">Request returned 401 (Unauthorized)</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">Request returned 403 (Forbidden)</exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The request failed due to timeout.</exception>
    Task PostAsync(string resourcePath,
        CancellationToken cancellationToken);

    /// <summary>
    /// Make a Multi-Part request to the server, using the format required for Tethr to send Audio and MetaData
    /// </summary>
    /// <typeparam name="TIn">The object type of the body.</typeparam>
    /// <typeparam name="TOut">The object to fill with the result from the server.</typeparam>
    /// <param name="resourcePath">The path to the resource to get.</param>
    /// <param name="info">The content to be sterilized and send in the Info part of the request</param>
    /// <param name="inputJsonInfo">The Json Info from JsonSerializerContext for the body to send.</param>
    /// <param name="buffer">The binary data part of the request</param>
    /// <param name="outputJsonInfo">The Json Info from JsonSerializerContext for the type that should be returned.</param>
    /// <param name="dataPartMediaType">The MediaType of the binary data being sent to the server</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>TOut filled with the result from the server.</returns>
    /// <exception cref="T:Tethr.Sdk.TethrSessionProcessingException">Thrown when a critical error occurs within the Tethr SDK, indicating a failure in session processing. This exception highlights severe issues that disrupt normal operation.</exception>
    /// <exception cref="T:System.Net.Http.HttpRequestException">The HTTP response is unsuccessful.</exception>
    /// <exception cref="T:System.Security.Authentication.AuthenticationException">Request returned 401 (Unauthorized)</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">Request returned 403 (Forbidden)</exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The request failed due to timeout.</exception>
    Task<TOut> PostMultiPartAsync<TIn, TOut>(string resourcePath,
        TIn info,
        JsonTypeInfo<TIn> inputJsonInfo,
        Stream buffer,
        JsonTypeInfo<TOut> outputJsonInfo,
        CancellationToken cancellationToken,
        string dataPartMediaType = "application/octet-stream");

    /// <summary>
    /// Make a GET request to the server and return an output stream.
    /// </summary>
    /// <param name="resourcePath">The path to the resource to get.</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>A stream containing the results of the GET</returns>
    /// <exception cref="T:Tethr.Sdk.TethrSessionProcessingException">Thrown when a critical error occurs within the Tethr SDK, indicating a failure in session processing. This exception highlights severe issues that disrupt normal operation.</exception>
    /// <exception cref="T:System.Net.Http.HttpRequestException">The HTTP response is unsuccessful.</exception>
    /// <exception cref="T:System.Security.Authentication.AuthenticationException">Request returned 401 (Unauthorized)</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">Request returned 403 (Forbidden)</exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The request failed due to timeout.</exception>
    Task<Stream> GetStreamAsync(
        string resourcePath,
        CancellationToken cancellationToken);
}