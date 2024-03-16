using Tethr.Sdk.Model;
using Tethr.Sdk.Session;

namespace Tethr.Sdk;

public interface ITethrSessionStatus
{
    Task<SessionStatus?> GetSessionStatusAsync(string sessionId);
    Task<SessionStatusResponse> GetSessionStatusAsync(IEnumerable<string> sessionIds);
    Task SetExcludedStatusAsync(string sessionId);
    Task SetExcludedStatusAsync(IEnumerable<string> sessionIds);
}

public class TethrSessionStatus(ITethrSession tethrSession) : ITethrSessionStatus
{
    public async Task<SessionStatus?> GetSessionStatusAsync(string sessionId)
    {
        return (await GetSessionStatusAsync(new[] { sessionId }).ConfigureAwait(false)).CallSessions
            .FirstOrDefault();
    }

    public async Task<SessionStatusResponse> GetSessionStatusAsync(IEnumerable<string> sessionIds)
    {
        ArgumentNullException.ThrowIfNull(sessionIds, nameof(sessionIds));
        
        var result = await
            tethrSession.PostAsync("/callCapture/v1/status",
                    new SessionRequest { CallSessionIds = sessionIds },
                    TethrModelSerializerContext.Default.SessionRequest,
                    TethrModelSerializerContext.Default.SessionStatusResponse)
                .ConfigureAwait(false);

        return result;
    }

    public async Task SetExcludedStatusAsync(string sessionId)
    {
        if(string.IsNullOrEmpty(sessionId)) throw new ArgumentNullException(nameof(sessionId));
        await SetExcludedStatusAsync(new[] { sessionId }).ConfigureAwait(false);
    }

    public async Task SetExcludedStatusAsync(IEnumerable<string> sessionIds)
    {
        ArgumentNullException.ThrowIfNull(sessionIds, nameof(sessionIds));
        await tethrSession.PostAsync("/callCapture/v1/status/exclude",
                new SessionRequest { CallSessionIds = sessionIds },
                TethrModelSerializerContext.Default.SessionRequest)
            .ConfigureAwait(false);
    }
}