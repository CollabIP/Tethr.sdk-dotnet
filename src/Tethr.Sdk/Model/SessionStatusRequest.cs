namespace Tethr.Sdk.Model;

public class SessionStatusRequest
{
    /// <summary>
    /// The session Ids for the interactions.
    /// </summary>
    public IEnumerable<string> SessionIds { get; set; } = Array.Empty<string>();
}