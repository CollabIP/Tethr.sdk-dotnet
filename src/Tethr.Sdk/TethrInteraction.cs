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
    /// <param name="request">The specified customer phone numbers and/or emails. Phone formats must match that of what is in Tethr.</param>
    /// <param name="whatIf">When set to true, Tethr will return a list of interactions that would have been purged. Purge status in the response will be listed as 'WhatIf' when used.</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>An array containing the purge status of the requested calls</returns>
    public async Task<InteractionPurgeSummary> PurgeByCustomerIdsAsync(InteractionPurgeByCustomerMetadataRequest request,
        bool whatIf = false,
        CancellationToken cancellationToken = default)
    {
        var resourcePath = "/interactions/v2/purge/customer";
        if (whatIf)
        {
            resourcePath += "?whatIf=true";
        }
        
        var result = await
            tethrSession.PostAsync(
                resourcePath,
                request,
                TethrModelSerializerContext.Default.InteractionPurgeByCustomerMetadataRequest,
                TethrModelSerializerContext.Default.InteractionPurgeSummary,
                cancellationToken).ConfigureAwait(false);
        return result;
    }
}