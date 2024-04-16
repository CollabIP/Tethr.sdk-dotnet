using System.ComponentModel.DataAnnotations;

namespace Tethr.Sdk.Model;

/// <summary>
/// Metadata to append to all interactions with the specified master Id.
/// </summary>
public class AsyncMetadataMasterRequest : AsyncMetadataInteractionBase
{
    /// <summary>
    /// The master Id the metadata should be appended to.
    /// </summary>
    [Required]
    public string? MasterId { get; set; }
}