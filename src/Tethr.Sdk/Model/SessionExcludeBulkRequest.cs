namespace Tethr.Sdk.Model;

public class SessionExcludeBulkRequest
{
    /// <summary>
    /// The session Ids for the interactions.
    /// </summary>
    public IEnumerable<string> SessionIds { get; set; } = Array.Empty<string>();
}