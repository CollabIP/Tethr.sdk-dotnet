namespace Tethr.Sdk.Model;

public class SessionRequest
{
    public IEnumerable<string> CallSessionIds { get; set; } = Array.Empty<string>();
}