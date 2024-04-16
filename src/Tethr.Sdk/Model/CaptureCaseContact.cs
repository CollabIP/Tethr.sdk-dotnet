using System.ComponentModel.DataAnnotations;

namespace Tethr.Sdk.Model
{
	/// <summary>
	/// The contact that participates in a message.
	/// </summary>
	public class CaptureCaseContact
	{
		/// <summary>
		/// A Reference Id or name for the contact. 
		/// </summary>
		[Required]
		public string? ReferenceId { get; set; }

		/// <summary>
		/// Optional First name of the contact
		/// </summary>
		public string? FirstName { get; set; } 

		/// <summary>
		/// Optional Last name of the contact
		/// </summary>
		public string? LastName { get; set; } 

		/// <summary>
		/// The type of contact. Default types are "Agent" and "Customer", but other types can be configured in Tethr for IVRs or automated attendants.  See participants types in Tethr Documentation.
		/// </summary>
		[Required]
		public string? Type { get; set; }
		
		/// <summary>
		/// The channel number in the recording that this contact is associated with.
		/// <para>
		/// This is 0 based, so the first channel is 0, the second is 1, etc.
		/// </para>
		/// <para>
		/// All channels with the recording should be accounted for, and there can not be duplicates entries.
		/// </para>
		/// </summary>
		[Required]
		public int Channel { get; set; }
		
		/// <summary>
		/// Optional phone number to be listed for the contact.
		/// </summary>
		public string? PhoneNumber { get; set; }

		/// <summary>
		/// Optional email to be listed for the contact.
		/// </summary>
		public string? Email { get; set; }
	}
}