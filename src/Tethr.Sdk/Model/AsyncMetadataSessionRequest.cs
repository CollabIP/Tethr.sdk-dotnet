using System.ComponentModel.DataAnnotations;

namespace Tethr.Sdk.Model;

/// <summary>
/// Metadata to append to the interactions with the specified session Id.
/// </summary>
public class AsyncMetadataSessionRequest : AsyncMetadataInteractionBase
{
    /// <summary>
    /// The session Id for the call or chat this metadata should be appended to.
    /// </summary>
    [Required]
    public string? SessionId { get; set; }
}