using Tethr.Sdk.Model;
using Tethr.Sdk.Session;

namespace Tethr.Sdk;

public class TethrCapture(ITethrSession tethrSession)
{
    private static readonly Dictionary<string, string> MimeTypeMappings = new()
    {
        { "audio/wav", "wav" },
        { "audio/x-wav", "wav" },
        { "audio/wave", "wav" },
        { "audio/vnd.wav", "wav" },
        { "audio/x-wave", "wav" },
        { "audio/mp3", "mp3" },
        { "audio/ogg", "opus" },
        { "audio/mp4", "mp4" },
        { "audio/m4a", "mp4" },
        { "audio/mp4-helium", "mp4helium" },
        { "audio/m4a-helium", "mp4helium" },
        { "audio/wma", "wma" },
        { "audio/wma-helium", "wmahelium" }
    };

    private static string MediaTypeToTethrType(string mimeType)
    {
        if (string.IsNullOrEmpty(mimeType)) throw new ArgumentNullException(nameof(mimeType));
        return MimeTypeMappings.GetValueOrDefault(mimeType.ToLower(), mimeType);
    }

    public async Task<CaptureResponse> UploadAsync(
        CaptureCallRequest info,
        Stream waveStream,
        string mediaType,
        CancellationToken cancellationToken = default)
    {
        // Check for required parameters
        ArgumentNullException.ThrowIfNull(info, nameof(info));
        ArgumentNullException.ThrowIfNull(waveStream, nameof(waveStream));
        if (string.IsNullOrEmpty(mediaType)) throw new ArgumentNullException(nameof(mediaType));

        if (string.IsNullOrEmpty(info.SessionId)) throw new ArgumentNullException(nameof(info.SessionId));
        if (info.MasterId is { Length: 0 }) info.MasterId = null;
        if (info.Participants.Count == 0) throw new ArgumentNullException(nameof(info.Participants));
        if (info.UtcStart == default) throw new ArgumentNullException(nameof(info.UtcStart));

        info.UtcStart = info.UtcStart.Kind switch
        {
            DateTimeKind.Unspecified
                => throw new ArgumentException("Start time must be in UTC", paramName: nameof(info.UtcStart)),
            DateTimeKind.Local
                => info.UtcStart.ToUniversalTime(),
            _ => info.UtcStart
        };

        if (info.UtcEnd.HasValue)
        {
            info.UtcEnd = info.UtcEnd.Value.Kind switch
            {
                DateTimeKind.Unspecified
                    => throw new ArgumentException("End time must be in UTC", paramName: nameof(info.UtcEnd)),
                DateTimeKind.Local
                    => info.UtcEnd.Value.ToUniversalTime(),
                _ => info.UtcEnd
            };

            if (info.UtcStart < info.UtcEnd)
                throw new ArgumentException("Start time cannot be greater than end time",
                    paramName: nameof(info.UtcEnd));
        }

        // check the media types, and convert them to the types that Tethr is expecting.
        // Allow the user to put in other types, that maybe the API supports now but the SDK doesn't
        // yet have the mapping from media type.
        if (string.IsNullOrEmpty(info.Audio.Format))
        {
            var audioFormat = MediaTypeToTethrType(mediaType);
            info.Audio.Format = audioFormat;
        }

        var result = await
            tethrSession.PostMultiPartAsync("/capture/v2/call",
                info,
                TethrModelSerializerContext.Default.CaptureCallRequest,
                waveStream,
                TethrModelSerializerContext.Default.CaptureResponse,
                cancellationToken,
                mediaType).ConfigureAwait(false);

        return result;
    }

    public async Task<CaptureResponse> UploadAsync(CaptureChatRequest captureChatRequest,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(captureChatRequest, nameof(captureChatRequest));
        if (string.IsNullOrEmpty(captureChatRequest.SessionId))
            throw new ArgumentNullException(nameof(captureChatRequest.SessionId));
        if (captureChatRequest.Participants.Count == 0)
            throw new ArgumentNullException(nameof(captureChatRequest.Participants));
        if (captureChatRequest.UtcStart == default)
            throw new ArgumentNullException(nameof(captureChatRequest.UtcStart));
        if (captureChatRequest.UtcEnd == default) throw new ArgumentNullException(nameof(captureChatRequest.UtcEnd));

        captureChatRequest.UtcStart = captureChatRequest.UtcStart.Kind switch
        {
            DateTimeKind.Unspecified
                => throw new ArgumentException("Start time must be in UTC", nameof(captureChatRequest.UtcStart)),
            DateTimeKind.Local
                => captureChatRequest.UtcStart.ToUniversalTime(),
            _ => captureChatRequest.UtcStart
        };

        captureChatRequest.UtcEnd = captureChatRequest.UtcEnd.Kind switch
        {
            DateTimeKind.Unspecified
                => throw new ArgumentException("End time must be in UTC", nameof(captureChatRequest.UtcEnd)),
            DateTimeKind.Local
                => captureChatRequest.UtcEnd.ToUniversalTime(),
            _ => captureChatRequest.UtcEnd
        };

        if (captureChatRequest.UtcStart > captureChatRequest.UtcEnd)
            throw new ArgumentException("Start time cannot be greater than end time");

        foreach (var chatSessionContact in captureChatRequest.Participants)
        {
            if (string.IsNullOrEmpty(chatSessionContact.ReferenceId))
                throw new ArgumentNullException(nameof(chatSessionContact.ReferenceId));
            if (string.IsNullOrEmpty(chatSessionContact.Type))
                throw new ArgumentNullException(nameof(chatSessionContact.Type));

            foreach (var message in chatSessionContact.Messages)
            {
                if (message.UtcTimestamp == default)
                    throw new ArgumentNullException(nameof(message.UtcTimestamp));
                if (message.UtcTimestamp.Kind != DateTimeKind.Utc)
                    throw new ArgumentException("Message timestamp must be in UTC", nameof(message.UtcTimestamp));
                if (message.UtcTimestamp < captureChatRequest.UtcStart ||
                    message.UtcTimestamp > captureChatRequest.UtcEnd)
                    throw new ArgumentException(
                        "Message timestamp must be within the chat session start and end times",
                        nameof(message.UtcTimestamp));
                if (message.Content == null)
                    throw new ArgumentNullException(nameof(message.Content));
            }
        }

        var result = await
            tethrSession.PostAsync(
                "/capture/v2/chat",
                captureChatRequest,
                TethrModelSerializerContext.Default.CaptureChatRequest,
                TethrModelSerializerContext.Default.CaptureResponse, cancellationToken).ConfigureAwait(false);
        return result;
    }

    public async Task<CaptureResponse> UploadAsync(CaptureCaseRequest captureCaseRequest,
        CancellationToken cancellationToken = default)
    {
        var result = await
            tethrSession.PostAsync(
                "/capture/v2/chat",
                captureCaseRequest,
                TethrModelSerializerContext.Default.CaptureCaseRequest,
                TethrModelSerializerContext.Default.CaptureResponse, cancellationToken).ConfigureAwait(false);
        return result;
    }

    public async Task UploadAsync(CaptureStatusHeartbeatRequest heartbeatRequest,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(heartbeatRequest, nameof(heartbeatRequest));
        
        await
            tethrSession.PostAsync("/capture/v2/status/monitor",
                    heartbeatRequest,
                    TethrModelSerializerContext.Default.CaptureStatusHeartbeatRequest,
                    cancellationToken)
                .ConfigureAwait(false);
    }
    
    public async Task<SessionStatus> GetSessionStatusAsync(string sessionId,
        CancellationToken cancellationToken = default)
    {
        var result = await
            tethrSession.GetAsync($"/capture/v2/status/{sessionId}",
                TethrModelSerializerContext.Default.SessionStatus, cancellationToken)
                .ConfigureAwait(false);

        return result;
    }

    public async Task<SessionStatusResponse> GetSessionStatusAsync(IEnumerable<string> sessionIds,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(sessionIds, nameof(sessionIds));
        
        var result = await
            tethrSession.PostAsync("/capture/v2/status",
                    new SessionStatusRequest { SessionIds = sessionIds },
                    TethrModelSerializerContext.Default.SessionStatusRequest,
                    TethrModelSerializerContext.Default.SessionStatusResponse, cancellationToken)
                .ConfigureAwait(false);

        return result;
    }
    
    public async Task SetSessionExcludedAsync(string sessionId,
        CancellationToken cancellationToken = default)
    {
        await
            tethrSession.PostAsync($"/capture/v2/status/exclude/{sessionId}", cancellationToken)
                .ConfigureAwait(false);
    }

    public async Task SetSessionExcludedAsync(IEnumerable<string> sessionIds,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(sessionIds, nameof(sessionIds));
        
        await
            tethrSession.PostAsync("/capture/v2/status/exclude",
                    new SessionExcludeBulkRequest { SessionIds = sessionIds },
                    TethrModelSerializerContext.Default.SessionExcludeBulkRequest,
                    cancellationToken)
                .ConfigureAwait(false);
    }
}