namespace Tethr.Sdk.Model
{
    public class SessionStatus
    {
        public SessionStatuses Status { get; set; }

        public string CallId { get; set; } = string.Empty;

        public string SessionId { get; set; } = string.Empty;
        
        public long DurationSec { get; set; }
    }
}