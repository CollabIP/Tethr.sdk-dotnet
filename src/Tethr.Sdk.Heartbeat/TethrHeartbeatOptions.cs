namespace Tethr.Sdk.Heartbeat;

public sealed class TethrHeartbeatOptions
{
    /// <summary>
    /// How many seconds to wait between sending heartbeat requests to the server.
    /// </summary>
    /// <remarks>
    /// Is only used if the Tethr Heartbeat background service is enabled.
    /// </remarks>
    public int? HeartbeatIntervalSeconds { get; set; } = 60;
}