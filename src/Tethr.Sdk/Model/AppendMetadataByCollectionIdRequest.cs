using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;

namespace Tethr.Sdk.Model;

/// <summary>
/// Metadata to append to the case with the specified collection Id.
/// </summary>
public class AppendMetadataByCollectionIdRequest
{
    /// <summary>
    /// The Case reference Id, from the source system, this metadata should be appended to.
    /// </summary>
    [Required]
    public string CollectionId { get; set; } = string.Empty;

    /// <summary>
    /// The time stamp of the event this metadata represents.
    /// </summary>
    /// <remarks>
    /// Async Metadata is applied in sequential orders, so if two events contain the same field the value from the later event will be used.
    /// </remarks>
    public DateTime? EventTime { get; set; }

    /// <summary>
    /// The free-form JSON object that will be appended to the interaction metadata.
    /// </summary>
    [Required]
    public JsonNode? Metadata { get; set; }
}
