using System.ComponentModel.DataAnnotations;

namespace Tethr.Sdk.Model;

public class InteractionPurgeRequest
{
    /// <summary>
    /// List of IDs to purge
    /// </summary>
    [Required]
    public List<string> Ids { get; set; } = new();
		
    /// <summary>
    /// If an Id specified is still processing, attempt to purge it anyway, depending on the state of the call, this may or may not be successful.
    /// </summary>,
    public bool AllowIncompleteInteraction { get; set; }
}