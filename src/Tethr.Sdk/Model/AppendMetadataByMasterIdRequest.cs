using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;

namespace Tethr.Sdk.Model;

/// <summary>
/// Metadata to append to all interactions with the specified master Id.
/// </summary>
public class AppendMetadataByMasterIdRequest
{
    /// <summary>
    /// The master Id the metadata should be appended to.
    /// </summary>
    [Required]
    public string MasterId { get; set; } = string.Empty;

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
