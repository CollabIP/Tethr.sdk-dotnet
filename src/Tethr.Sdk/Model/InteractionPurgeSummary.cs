using System.ComponentModel.DataAnnotations;

namespace Tethr.Sdk.Model;

public class InteractionPurgeSummary
{
    /// <summary>
    /// Status of each purge job for each requested integration
    /// </summary>
    [Required]
    public List<InteractionPurgeSummaryItem> Items { get; set; } = new();
}

public class InteractionPurgeSummaryItem
{
    /// <summary>
    /// Tethr interaction ID (can be null if requested session ID wasn't found)
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Tethr session ID (can be null if requested interaction ID wasn't found)
    /// </summary>
    public string? SessionId { get; set; }

    /// <summary>
    /// Status of purge request
    /// </summary>
    [Required]
    public InteractionPurgeStatus Status { get; set; }

    /// <summary>
    /// Error message, if any
    /// </summary>
    public string? ErrorMsg { get; set; }
}
