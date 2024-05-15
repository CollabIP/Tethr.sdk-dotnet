using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;

namespace Tethr.Sdk.Model;

/// <summary>
/// Metadata to append to an interaction with the specified session Id.
/// </summary>
public class AppendMetadataBySessionIdRequest
{
    /// <summary>
    /// The session Id for the call or chat this metadata should be appended to.
    /// </summary>
    [Required]
    public string SessionId { get; set; } = string.Empty;

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
