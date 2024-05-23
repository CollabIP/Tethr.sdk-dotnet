using System.ComponentModel.DataAnnotations;

namespace Tethr.Sdk.Model;

/// <summary>
/// Purge calls by customer phone numbers and/or emails
/// </summary>
public class InteractionPurgeByCustomerMetadataRequest
{
    /// <summary>
    /// If an Id specified is still processing, attempt to purge it anyway, depending on the state of the call, this may or may not be successful.
    /// </summary>
    public bool AllowIncompleteInteraction { get; set; }
		
    /// <summary>
    /// List of phone numbers to purge, Tethr will look up any interactions where the provided phones is found on an external participant.
    /// </summary>
    public List<string> PhoneNumbers { get; set; }

    /// <summary>
    /// List of email address to purge, Tethr will look up any interactions where the provided email address is found on an external participant.
    /// </summary>
    [EmailAddress]
    public List<string> Emails { get; set; }
}