using Tethr.Sdk.Model;
using Tethr.Sdk.Session;

namespace Tethr.Sdk
{
    /// <summary>
    /// Interface for working with call archives where the call recording has already completed and the audio is available for the entire call.
    /// </summary>
    public interface ITethrArchivedRecording
    {
        Task<InteractionResponse> SendRecordingAsync(ArchivedRecordingInfo info, Stream waveStream, string mediaType);
    }

    public class TethrArchivedRecording(ITethrSession tethrSession) : ITethrArchivedRecording
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

        public async Task<InteractionResponse> SendRecordingAsync(
            ArchivedRecordingInfo info,
            Stream waveStream,
            string mediaType)
        {
            // Check for required parameters
            ArgumentNullException.ThrowIfNull(info, nameof(info));
            ArgumentNullException.ThrowIfNull(waveStream, nameof(waveStream));
            if (string.IsNullOrEmpty(mediaType)) throw new ArgumentNullException(nameof(mediaType));

            if (string.IsNullOrEmpty(info.SessionId)) throw new ArgumentNullException(nameof(info.SessionId));
            if (info.MasterCallId is { Length: 0 }) info.MasterCallId = null;
            if (info.Contacts.Count == 0) throw new ArgumentNullException(nameof(info.Contacts));
            if (info.StartTime == default) throw new ArgumentNullException(nameof(info.StartTime));
            if (info.EndTime == default) throw new ArgumentNullException(nameof(info.StartTime));
            if (info.Direction == CallDirection.Invalid) throw new ArgumentNullException(nameof(info.Direction));
            
            // check the media types, and convert them to the types that Tethr is expecting.
            // Allow the user to put in other types, that maybe the API supports now but the SDK doesn't
            // yet have the mapping from media type.
            var audioFormat = MediaTypeToTethrType(mediaType);
            info.Audio = new Audio { Format = audioFormat };

            var result = await
                tethrSession.PostMultiPartAsync("/callCapture/v1/archive",
                    info,
                    TethrModelSerializerContext.Default.ArchivedRecordingInfo,
                    waveStream,
                    TethrModelSerializerContext.Default.InteractionResponse,
                    mediaType).ConfigureAwait(false);

            return result;
        }
    }
}