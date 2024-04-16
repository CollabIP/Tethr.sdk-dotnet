using Tethr.Sdk.Model;
using Tethr.Sdk.Session;

namespace Tethr.Sdk;

public class TethrProcessing(ITethrSession tethrSession)
{
    /// <summary>
    /// Gets a list of all the categories that can be processed along with information about them.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of category info items</returns>
    public async Task<CategoryInfoResponse> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var result = await
            tethrSession.GetAsync($"/processing/v2/categories",
                    TethrModelSerializerContext.Default.CategoryInfoResponse, cancellationToken)
                .ConfigureAwait(false);

        return result;
    }

    public async Task<CustomFieldResponse> GetCustomFieldsAsync(CancellationToken cancellationToken = default)
    {
        var result = await
            tethrSession.GetAsync($"/processing/v2/customFields",
                    TethrModelSerializerContext.Default.CustomFieldResponse, cancellationToken)
                .ConfigureAwait(false);

        return result;
    }

    public async Task<ParticipantTypesResponse> GetParticipantTypesAsync(CancellationToken cancellationToken = default)
    {
        var result = await
            tethrSession.GetAsync($"/processing/v2/ParticipantRefTypes",
                    TethrModelSerializerContext.Default.ParticipantTypesResponse, cancellationToken)
                .ConfigureAwait(false);

        return result;
    }
}