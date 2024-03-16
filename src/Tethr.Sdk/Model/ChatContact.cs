namespace Tethr.Sdk.Model
{
	public class ChatContact
	{
		/// <summary>
		/// The ID of the contact. 
		/// This is a string that uniquely identifies the contact
		/// </summary>
		/// <remarks>
		/// Is used a part of a two part key along with the Type property used to identify a contact.
		/// </remarks>
		public string ReferenceId { get; set; }

		/// <summary>
		/// The contact's first name
		/// </summary>
		public string FirstName { get; set; }
		
		/// <summary>
		/// The contact's last name
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// What type of participant is this contact
		/// </summary>
		/// <example> Default types: 
		/// Agent - for an internal participant,
		/// Customer - for a customer,
		/// System - for automaticly generate chat messages,
		/// </example>
		/// <remarks>
		/// The list of Types can be defend in Tethr if new or different types are wanted.  The only predefined types are Agent and Customer.
		/// At this time only one Contact is allowed per Channel
		/// 
		/// At this time only one Contact is allowed per Type on a given call.
		/// </remarks>
		public string Type { get; set; }
		
		public List<ChatMessage> Messages { get; set; } 
	}
}