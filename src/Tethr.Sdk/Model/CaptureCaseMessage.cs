using System.ComponentModel.DataAnnotations;

namespace Tethr.Sdk.Model;

/// <summary>
/// The text message details.
/// </summary>
public class CaptureCaseMessage
{
    /// <summary>
    /// The datetime of the message in UTC.
    /// </summary>
    [Required] 
    public DateTime UtcTimestamp { get; set; }

    /// <summary>
    /// <para>
    /// Reference ID of the contact who sent the message.
    /// </para><para>
    /// There must be a corresponding contact with this reference ID in the contacts list.
    /// </para>
    /// </summary>
    [Required]
    public string? SenderReferenceId { get; set; }

    /// <summary>
    /// <para>
    /// The communication channel this message was sent on, example "Web" or "email".
    /// </para><para>
    /// Many cases systems have support for multiple channels, for example a case may have a message from a web form that user can fill out, or a user may email in a message.
    /// </para>
    /// </summary>
    [Required]
    public string? Channel { get; set; } 

    /// <summary>
    /// The message body or content of the interaction.
    /// </summary>
    [Required]
    public string? Content { get; set; }
}