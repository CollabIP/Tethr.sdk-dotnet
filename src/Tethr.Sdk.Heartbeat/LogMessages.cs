using Microsoft.Extensions.Logging;

namespace Tethr.Sdk.Heartbeat;


internal static partial class LogMessages
{
    private const string EventIdPrefix = "Tethr.Sdk.Heartbeat";
    private const int EventIdOffset = 35000;

    [LoggerMessage(EventIdOffset + 1, LogLevel.Information,
        "Tethr heart beat interval set to {HeartbeatIntervalSeconds}"
        , EventName = $"{EventIdPrefix}.SetHeartbeatIntervalSeconds")]
    public static partial void SetHeartbeatIntervalSeconds(this ILogger<TethrHeartbeatService> logger, long heartbeatIntervalSeconds);

    [LoggerMessage(EventId = EventIdOffset + 2, Level = LogLevel.Information,
        Message = "Error handling change to Tethr heart beat options",
        EventName = $"{EventIdPrefix}.ErrorUpdatingOptions")]
    public static partial void ErrorUpdatingOptions(this ILogger<TethrHeartbeatService> logger, Exception ex);
    
    [LoggerMessage(EventId = EventIdOffset + 3, Level = LogLevel.Warning,
        Message = "Error sending Tethr heart beat",
        EventName = $"{EventIdPrefix}.ErrorSendingHeartBeat")]
    public static partial void ErrorSendingHeartBeat(this ILogger<TethrHeartbeatService> logger, Exception ex);
    
    [LoggerMessage(EventId = EventIdOffset + 4, Level = LogLevel.Information,
        Message = "Tethr heart beat reestablished",
        EventName = $"{EventIdPrefix}.HeartBeatReestablished")]
    public static partial void HeartBeatReestablished(this ILogger<TethrHeartbeatService> logger);
    
    // Log for HeartBeatStopped
    [LoggerMessage(EventId = EventIdOffset + 5, Level = LogLevel.Information,
        Message = "Tethr heart beat stopped",
        EventName = $"{EventIdPrefix}.HeartBeatStopped")]
    public static partial void HeartBeatStopped(this ILogger<TethrHeartbeatService> logger);
}