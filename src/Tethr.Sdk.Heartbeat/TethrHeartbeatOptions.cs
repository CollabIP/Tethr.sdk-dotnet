using Tethr.Sdk.Model;

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

    /// <summary>
    /// Setup a call back for checking the status health of the broker.
    /// </summary>
    /// <remarks>
    /// This allows for the broker to check other internal status indicators to see if the system is healthy.
    /// If a call back is not set, a default value of Healthy will be sent to Tethr.
    /// </remarks>
    public Func<MonitorStatus>? StatusCallback { get; set; }
}