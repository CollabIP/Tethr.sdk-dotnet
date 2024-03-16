using Tethr.Sdk.Model;
using Tethr.Sdk.Session;

namespace Tethr.Sdk
{
    /// <summary>
    /// Interface for updating metadata for interactions.
    /// </summary>
    public interface ITethrAsyncMetadata
    {
        /// <summary>
        /// Update metadata for the interaction with the specified session Id.
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        Task SendInteractionMetadataBySessionIdAsync(SessionInteractionMetadata metadata);

        /// <summary>
        /// Update metadata for all interactions with the specified master call Id.
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        Task SendInteractionMetadataByMasterCallIdAsync(MasterInteractionMetadata metadata);

        /// <summary>
        /// Update metadata for the case with the specified case reference Id.
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        Task SendInteractionMetadataByCaseReferenceIdAsync(CaseInteractionMetadata metadata);
    }

    public class TethrAsyncMetadata(ITethrSession tethrSession) : ITethrAsyncMetadata
    {
        /// <inheritdoc/>
        public async Task SendInteractionMetadataBySessionIdAsync(SessionInteractionMetadata metadata)
        {
            ArgumentNullException.ThrowIfNull(metadata, nameof(metadata));
            await tethrSession.PostAsync("/callEvent/v1/outofband/event", metadata,
                TethrModelSerializerContext.Default.SessionInteractionMetadata).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task SendInteractionMetadataByMasterCallIdAsync(MasterInteractionMetadata metadata)
        {
            ArgumentNullException.ThrowIfNull(metadata, nameof(metadata));
            await tethrSession.PostAsync("/callEvent/v1/outofband/masterCall", metadata,
                TethrModelSerializerContext.Default.MasterInteractionMetadata).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task SendInteractionMetadataByCaseReferenceIdAsync(CaseInteractionMetadata metadata)
        {
            ArgumentNullException.ThrowIfNull(metadata, nameof(metadata));
            await tethrSession.PostAsync("/callEvent/v1/outofband/case", metadata,
                TethrModelSerializerContext.Default.CaseInteractionMetadata).ConfigureAwait(false);
        }
    }
}