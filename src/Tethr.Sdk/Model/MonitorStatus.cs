namespace Tethr.Sdk.Model;

public enum MonitorStatus
{
    /// <summary>
    /// The status is unknown or not specified at this time.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// Everything is working as expected.
    /// </summary>
    Healthy = 1,
    /// <summary>
    /// The system is online, but something is out of normal parameters.
    /// </summary>
    Warning = 10,
    /// <summary>
    /// The system is online, but there are errors that should be looked at.
    /// </summary>
    Error = 20,
    /// <summary>
    /// The system is off-line, and unable to process calls at this time.
    /// </summary>
    Offline = 30
}
