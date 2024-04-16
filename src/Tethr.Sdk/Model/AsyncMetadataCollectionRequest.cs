using System.ComponentModel.DataAnnotations;

namespace Tethr.Sdk.Model;

/// <summary>
/// Metadata to append to the case with the specified collection Id.
/// </summary>
public class AsyncMetadataCollectionRequest : AsyncMetadataInteractionBase
{
    /// <summary>
    /// The Case reference Id, from the source system, this metadata should be appended to.
    /// </summary>
    [Required]
    public string? CollectionId { get; set; }
}