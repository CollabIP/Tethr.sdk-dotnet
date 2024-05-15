using System.ComponentModel.DataAnnotations;

namespace Tethr.Sdk.Model;

public class CaptureChatContact
{
	/// <summary>
	/// The ID of the contact. 
	/// This is a string that uniquely identifies the contact
	/// </summary>
	/// <remarks>
	/// Is used a part of a two part key along with the Type property used to identify a contact.
	/// </remarks>
	public string? ReferenceId { get; set; }

	/// <summary>
	/// The contact's first name
	/// </summary>
	public string? FirstName { get; set; }
		
	/// <summary>
	/// The contact's last name
	/// </summary>
	public string? LastName { get; set; }

	/// <summary>
	/// What type of participant is this contact
	/// </summary>
	/// <example>
	/// <para>Agent - for an internal participant</para>
	/// <para>Customer - for a customer</para>
	/// <para>System - for automatically generate chat messages</para>
	/// <para>Bot - for virtual chat agents</para>
	/// </example>
	/// <remarks>
	/// The list of Types can be defined in Tethr if new or different types are wanted.  The only predefined types are Agent and Customer.
	/// At this time only one Contact is allowed per Channel
	/// </remarks>
	[Required]
	public string? Type { get; set; }

	/// <summary>
	/// Optional phone of the contact.
	/// </summary>
	public string? PhoneNumber { get; set; }

	/// <summary>
	/// Optional email of the contact.
	/// </summary>
	[EmailAddress]
	public string? Email { get; set; }

	public List<CaptureChatMessage> Messages { get; set; } = new();
}