using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Tethr.Sdk.Model;

namespace Tethr.Sdk;

public static class TethrExtensions
{
    public static async Task<CaptureResponse> UploadAsync(
        this TethrCapture tethrCapture,
        CaptureCallRequest info,
        string audioFileName,
        string mediaType,
        CancellationToken cancellationToken = default)
    {
        await using var wavStream = new FileStream(audioFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        return await tethrCapture.UploadAsync(info, wavStream, mediaType, cancellationToken).ConfigureAwait(false);
    }

    public static void SetMetadata<T>(this ITethrMetadata info, T metadata, JsonTypeInfo<T> metadataTypeInfo)
    {
        info.Metadata = JsonSerializer.SerializeToElement(metadata, metadataTypeInfo);
    }

    public static IEnumerable<IReadOnlyCollection<T>> BatchesOf<T>(this IEnumerable<T> sequence, int batchSize)
    {
        if (batchSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(batchSize), batchSize, "Batch size must be greater than 0.");
        }

        ArgumentNullException.ThrowIfNull(sequence, nameof(sequence));

        var batch = new List<T>(batchSize);
        foreach (var item in sequence)
        {
            batch.Add(item);
            if (batch.Count == batchSize)
            {
                yield return batch;
                batch = new List<T>(batchSize);
            }
        }

        if (batch.Count > 0)
        {
            yield return batch.ToArray();
            batch.Clear();
        }
    }
}