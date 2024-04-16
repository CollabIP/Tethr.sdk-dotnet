using Tethr.Sdk.Model;
using Tethr.Sdk.Session;

namespace Tethr.Sdk;

public class TethrInteraction(ITethrSession tethrSession) 
{
    public async Task<InteractionDetailsResponse> GetDetails(string id,
        bool isFlow,
        CancellationToken cancellationToken = default)
    {
        var resourcePath = $"/interactions/v2/{id}";
        if (isFlow)
        {
            resourcePath += "&isFlow=true";
        }
        
        var result = await
            tethrSession.GetAsync(resourcePath,
                    TethrModelSerializerContext.Default.InteractionDetailsResponse, cancellationToken)
                .ConfigureAwait(false);

        return result;
    }

    /// <summary>
    /// Get the audio for an interaction as MP3
    /// </summary>
    /// <param name="id">The Tethr interaction id for which to return the audio</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A stream containing the audio for the interaction.</returns>
    public async Task<Stream> GetAudioAsMp3(string id
        , CancellationToken cancellationToken = default)
    {
        var resourcePath = $"/interactions/v2/{id}/audio.mp3";
        return await tethrSession.GetStreamAsync(resourcePath, cancellationToken)
            .ConfigureAwait(false);
    }
    
    /// <summary>
    /// Create Share Token
    /// </summary>
    /// <remarks>
    /// The Share Token API endpoint is intended for applications
    /// in need of way to allow its user to review the full depth of insights provided with a specific Tethr interactions,
    /// but where the user of this application doesn't need full access to the Tethr platform directly.
    /// </remarks>
    public async Task<InteractionShareResponse> ShareCall(InteractionShareRequest interactionShareRequest
        , CancellationToken cancellationToken = default)
    {
        if (interactionShareRequest == null) throw new ArgumentNullException(nameof(interactionShareRequest));
        if (string.IsNullOrEmpty(interactionShareRequest.InteractionId)) throw new ArgumentNullException(nameof(interactionShareRequest.InteractionId));
        if (string.IsNullOrEmpty(interactionShareRequest.Email)) throw new ArgumentNullException(nameof(interactionShareRequest.Email));

        return await tethrSession.PostAsync("/Interactions/v2/token", interactionShareRequest,
                TethrModelSerializerContext.Default.InteractionShareRequest,
                TethrModelSerializerContext.Default.InteractionShareResponse, 
                cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Purge interactions specified by a list of interaction Ids.
    /// </summary>
    /// <param name="interactionPurgeRequest">A request containing a list of interaction Ids</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<InteractionPurgeSummary> PurgeByInteractionIdsAsync(InteractionPurgeRequest interactionPurgeRequest,
        CancellationToken cancellationToken = default)
    {
        var result = await
            tethrSession.PostAsync(
                "/interactions/v2/purge",
                interactionPurgeRequest,
                TethrModelSerializerContext.Default.InteractionPurgeRequest,
                TethrModelSerializerContext.Default.InteractionPurgeSummary,
                cancellationToken).ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// Purge interactions specified by a list of session Ids.
    /// </summary>
    /// <param name="interactionPurgeRequest">A request containing a list of session Ids</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<InteractionPurgeSummary> PurgeBySessionIdsAsync(InteractionPurgeRequest interactionPurgeRequest,
        CancellationToken cancellationToken = default)
    {
        var result = await
            tethrSession.PostAsync(
                "/interactions/v2/purge/sessions",
                interactionPurgeRequest,
                TethrModelSerializerContext.Default.InteractionPurgeRequest,
                TethrModelSerializerContext.Default.InteractionPurgeSummary,
                cancellationToken).ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// Purge interactions specified by a list of customer Ids (phone numbers or emails).
    /// </summary>
    /// <param name="interactionPurgeRequest">A request containing a list of customer phone numbers or emails</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<InteractionPurgeSummary> PurgeByCustomerIdsAsync(InteractionPurgeRequest interactionPurgeRequest,
        CancellationToken cancellationToken = default)
    {
        var result = await
            tethrSession.PostAsync(
                "/interactions/v2/purge/customer",
                interactionPurgeRequest,
                TethrModelSerializerContext.Default.InteractionPurgeRequest,
                TethrModelSerializerContext.Default.InteractionPurgeSummary,
                cancellationToken).ConfigureAwait(false);
        return result;
    }
}