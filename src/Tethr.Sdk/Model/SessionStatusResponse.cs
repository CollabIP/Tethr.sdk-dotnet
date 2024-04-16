namespace Tethr.Sdk.Model;

public class SessionStatusResponse
{
    public IEnumerable<SessionStatus> Sessions { get; set; } = Array.Empty<SessionStatus>();
}