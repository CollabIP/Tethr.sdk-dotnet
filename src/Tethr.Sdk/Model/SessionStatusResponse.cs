namespace Tethr.Sdk.Model;

public class SessionStatusResponse
{
    public IEnumerable<SessionStatus?> CallSessions { get; set; } = Array.Empty<SessionStatus?>();
}