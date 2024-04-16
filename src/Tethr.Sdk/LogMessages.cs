using Microsoft.Extensions.Logging;
using Tethr.Sdk.Session;

namespace Tethr.Sdk;

internal static partial class LogMessages
{
    private const string EventIdPrefix = "Tethr.Sdk";
    private const int EventIdOffset = 34000;

    [LoggerMessage(EventIdOffset + 1, LogLevel.Information,
        "Token received, type: {TokenType}, expires in {ExpiresInSeconds} seconds"
        , EventName = $"{EventIdPrefix}.TethrSession")]
    public static partial void TokenReceived(this ILogger<TethrSession> logger, string tokenType,
        long expiresInSeconds);

    [LoggerMessage(EventIdOffset + 2, LogLevel.Information,
        "Requests for Tethr to {HostUri} using SDK version {Version} {ProxyUri}",
        EventName = $"{EventIdPrefix}.RequestInitiated")]
    public static partial void RequestInitiated(this ILogger<TethrSession> logger, string hostUri, string version,
        string proxyUri);

    [LoggerMessage(EventIdOffset + 3, LogLevel.Warning,
        "Not able to get proxy",
        EventName = $"{EventIdPrefix}.ProxyError")]
    public static partial void ProxyError(this ILogger<TethrSession> logger, Exception ex);

    // Define a LoggerMessage for the HTTPS warning
    [LoggerMessage(EventIdOffset + 4, LogLevel.Warning,
        "Not using HTTPS for connection to server",
        EventName = $"{EventIdPrefix}.InsecureConnectionWarning")]
    public static partial void InsecureConnectionWarning(this ILogger<TethrSession> logger);
    
    [LoggerMessage(EventId = EventIdOffset + 5, Level = LogLevel.Debug,
        Message = "Making a request to {Uri}",
        EventName = $"{EventIdPrefix}.MakingRequest")]
    public static partial void MakingRequest(this ILogger<TethrSession> logger, Uri? uri);

    [LoggerMessage(EventId = EventIdOffset + 6, Level = LogLevel.Warning,
        Message = "Error processing change to options",
        EventName = $"{EventIdPrefix}.OptionsChangeError")]
    public static partial void ErrorUpdatingOptions(this ILogger<TethrSession> logger, Exception ex);
    
    [LoggerMessage(EventId = EventIdOffset + 7, Level = LogLevel.Warning,
        Message = "Error processing option, option {OptionName} is missing from the options",
        EventName = $"{EventIdPrefix}.OptionsChangeError")]
    public static partial void ErrorUpdatingOptionsMissingOption(this ILogger<TethrSession> logger, string optionName);
    
    [LoggerMessage(EventId = EventIdOffset + 8, Level = LogLevel.Debug,
        Message = "Successful upload of {InteractionType} interaction with session id {SessionId}, returned Tethr interaction id {InteractionId}",
        EventName = $"{EventIdPrefix}.TethrCapture")]
    public static partial void InteractionUploadEed(this ILogger<TethrCapture> logger, string interactionType, string sessionId, string interactionId, Exception ex);
}