using Tethr.Sdk.Model;
using Tethr.Sdk.Session;

namespace Tethr.Sdk
{
    public interface ITethrChat
    {
        Task<InteractionResponse> SendChatSessionAsync(ChatSession chatSession);
    }

    /// <summary>
    /// Client for sending Chat sessions to Tethr.
    /// </summary>
    public class TethrChat(ITethrSession tethrSession) : ITethrChat
    {
        public async Task<InteractionResponse> SendChatSessionAsync(ChatSession chatSession)
        {
            ArgumentNullException.ThrowIfNull(chatSession, nameof(chatSession));
            if (string.IsNullOrEmpty(chatSession.SessionId))
                throw new ArgumentNullException(nameof(chatSession.SessionId));
            if (chatSession.Contacts.Count == 0) throw new ArgumentNullException(nameof(chatSession.Contacts));
            if (chatSession.UtcStart == default) throw new ArgumentNullException(nameof(chatSession.UtcStart));
            if (chatSession.UtcEnd == default) throw new ArgumentNullException(nameof(chatSession.UtcEnd));
            
            var result = await
                tethrSession.PostAsync(
                    "/chatCapture/v1",
                    chatSession,
                    TethrModelSerializerContext.Default.ChatSession,
                    TethrModelSerializerContext.Default.InteractionResponse).ConfigureAwait(false);
            return result;
        }
    }
}