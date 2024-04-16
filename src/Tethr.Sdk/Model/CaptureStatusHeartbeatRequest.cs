namespace Tethr.Sdk.Model;

public class CaptureStatusHeartbeatRequest
{
    /// <summary>
    /// The current status of the service
    /// </summary>
    public MonitorStatus Status { get; set; }

    /// <summary>
    /// The local time on the service
    /// </summary>
    public DateTimeOffset TimeStamp { get; set; }

    /// <summary>
    /// A system name used to identify an instance of the service
    /// </summary>
    /// <remarks>
    /// Used only to detect if the system name has changed.
    /// There should only be one broker per API User, and this helps detect if a second one reporting.
    /// </remarks>
    public string? Name { get; set; }

    /// <summary>
    /// Software version number
    /// </summary>
    /// <remarks>
    /// Contains the version number of the broker 
    /// Example: "1.0.0.29"
    /// </remarks>
    public string? SoftwareVersion { get; set; }
}