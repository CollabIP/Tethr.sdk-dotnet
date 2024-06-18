namespace Tethr.Sdk.Model;

public class SessionStatus
{
    /// <summary>
    /// The processing status of the Tethr interaction for the requested session.
    /// </summary>
    public SessionStatuses Status { get; set; }

    /// <summary>
    /// The Tethr interaction id.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The session Id requested.
    /// </summary>
    public string SessionId { get; set; } = string.Empty;
        
    /// <summary>
    /// The duration of the interaction in seconds, if known.
    /// </summary>
    public long? DurationSec { get; set; }
}
