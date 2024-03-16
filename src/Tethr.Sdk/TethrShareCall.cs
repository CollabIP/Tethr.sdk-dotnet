using Tethr.Sdk.Model;
using Tethr.Sdk.Session;

namespace Tethr.Sdk
{
    /// <summary>
    /// Used to generate a link to a Tethr call for a guest user. This link expires in 8 hours by default but is configurable.
    /// </summary>
    public interface ITethrCallShare
    {
        /// <summary>
        /// Used to generate a link to a Tethr call for a guest user. This link expires in 8 hours by default but is configurable.
        /// </summary>
        Task<CallShareResponse> ShareCall(CallShare callShare);
    }

    /// <inheritdoc />
    public class TethrCallShare(ITethrSession tethrSession) : ITethrCallShare
    {
        /// <inheritdoc />
        public async Task<CallShareResponse> ShareCall(CallShare callShare)
        {
            if (callShare == null) throw new ArgumentNullException(nameof(callShare));
            if (string.IsNullOrEmpty(callShare.CallId)) throw new ArgumentNullException(nameof(callShare.CallId));
            if (string.IsNullOrEmpty(callShare.Email)) throw new ArgumentNullException(nameof(callShare.Email));

            return await tethrSession.PostAsync("callShare/v1/token", callShare,
                    TethrModelSerializerContext.Default.CallShare,
                    TethrModelSerializerContext.Default.CallShareResponse)
                .ConfigureAwait(false);
        }
    }
}