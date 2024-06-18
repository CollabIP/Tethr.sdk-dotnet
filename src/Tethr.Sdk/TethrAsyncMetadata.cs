using Tethr.Sdk.Model;
using Tethr.Sdk.Session;

namespace Tethr.Sdk;

public sealed class TethrAsyncMetadata(ITethrSession tethrSession)
{
    /// <summary>
    /// Update metadata for the interaction with the specified session Id.
    /// </summary>
    public async Task SendAsyncMetadataRequestAsync(AsyncMetadataSessionRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ArgumentNullException.ThrowIfNull(request.SessionId, nameof(request.SessionId));
        ArgumentNullException.ThrowIfNull(request.Metadata, nameof(request.Metadata));

        await tethrSession.PostAsync("capture/v2/outofband/interaction", request,
            TethrModelSerializerContext.Default.AsyncMetadataSessionRequest, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Update metadata for all interactions with the specified master Id.
    /// </summary>
    public async Task SendAsyncMetadataRequestAsync(AsyncMetadataMasterRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ArgumentNullException.ThrowIfNull(request.MasterId, nameof(request.MasterId));
        ArgumentNullException.ThrowIfNull(request.Metadata, nameof(request.Metadata));
        await tethrSession.PostAsync("/capture/v2/outofband/master", request,
            TethrModelSerializerContext.Default.AsyncMetadataMasterRequest, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Update metadata for the case with the specified case reference Id.
    /// </summary>
    public async Task SendAsyncMetadataRequestAsync(AsyncMetadataCaseRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ArgumentNullException.ThrowIfNull(request.CaseReferenceId, nameof(request.CaseReferenceId));
        ArgumentNullException.ThrowIfNull(request.Metadata, nameof(request.Metadata));
        await tethrSession.PostAsync("/capture/v2/outofband/case", request,
            TethrModelSerializerContext.Default.AsyncMetadataCaseRequest, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Append metadata to all interactions based on the Collection Id provided.
    /// </summary>
    public async Task SendAsyncMetadataRequestAsync(AsyncMetadataCollectionRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ArgumentNullException.ThrowIfNull(request.CollectionId, nameof(request.CollectionId));
        ArgumentNullException.ThrowIfNull(request.Metadata, nameof(request.Metadata));
        await tethrSession.PostAsync("/capture/v2/outofband/collection", request,
                TethrModelSerializerContext.Default.AsyncMetadataCollectionRequest, cancellationToken)
            .ConfigureAwait(false);
    }
}