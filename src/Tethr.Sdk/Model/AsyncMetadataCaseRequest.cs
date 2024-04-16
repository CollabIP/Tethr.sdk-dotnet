using System.ComponentModel.DataAnnotations;

namespace Tethr.Sdk.Model;

/// <summary>
/// Metadata to append to the case with the specified case reference Id.
/// </summary>
public class AsyncMetadataCaseRequest : AsyncMetadataInteractionBase
{
    /// <summary>
    /// The Case reference Id, from the source system, this metadata should be appended to.
    /// </summary>
    [Required]
    public string? CaseReferenceId { get; set; }
}