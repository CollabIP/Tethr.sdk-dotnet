using Tethr.Sdk.Model;
using Tethr.Sdk.Session;

namespace Tethr.Sdk
{
    public interface ITethrRecordingSettings
    {
        Task<IEnumerable<RecordingSettingSummary>> GetRecordingSettingsSummariesAsync();
    }

    public class TethrRecordingSettings(ITethrSession tethrSession) : ITethrRecordingSettings
    {
        public async Task<IEnumerable<RecordingSettingSummary>> GetRecordingSettingsSummariesAsync()
        {
            var result = await
                tethrSession.GetAsync(
                        "/sources/v1/recordingSettings",
                        TethrModelSerializerContext.Default.IEnumerableRecordingSettingSummary)
                    .ConfigureAwait(false);

            return result ?? Array.Empty<RecordingSettingSummary>();
        }
    }
}