using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Tethr.Sdk.Model
{
	/// <summary>
	/// Request for creating or updating a case.
	/// </summary>
	public class CaptureCaseRequest  : ITethrMetadata
	{
		/// <summary>
		/// <para>
		/// The external case ID from the source system.
		/// </para><para>
		///  When uploading future calls or chats that were a part of this case they should use this same reference ID as the CaseReferenceId.
		/// </para><para>
		/// Doing so will allow Tethr to bring them into the case, and you will be able to see all the components that make up this case in one view.
		/// This allows Tethr to understand all the components for the purpose of running analytics.
		/// </para>
		/// </summary>
		[Required]
		public string? ReferenceId { get; set; }

		/// <summary>
		/// <para>
		/// The ID to use to import the case as a composite interaction, with analysis
		/// </para><para>
		///	Cases with the same master ID will be treated as a single long overall case for the purposes of analysis.
		/// </para><para>This could be used if a follow-up case is created when a user replies via email to an already closed case,
		/// resulting in a new case being created in the case management systems, but for the purpose of analytics we may want
		/// to understand what's happening on a single unit.
		/// </para>
		/// </summary>
		public string? MasterId { get; set; }

		/// <summary>
		/// <para>
		/// An ID that Tethr will use to group together a set of interactions including cases, chats, or calls.
		/// </para><para>
		/// This is useful for grouping together interactions that are related, but should not be treated as a single long interaction for the purposes of analysis.
		/// </para>
		/// </summary>
		public string? CollectionId { get; set; }

		/// <inheritdoc/>
		public JsonElement? Metadata { get; set; }

		/// <summary>
		/// The datetime when the case was opened in UTC.
		/// </summary>
		[Required] public DateTime UtcStart { get; set; }

		/// <summary>
		/// The datetime when the case was closed in UTC.
		/// </summary>
		[Required] public DateTime UtcEnd { get; set; }

		/// <summary>
		/// The messages for the case.
		/// </summary>
		public List<CaptureCaseMessage> Messages { get; set; } = new();

		/// <summary>
		/// <para>
		/// The list of contacts that where an active party to the case, i.e. have a message attributed to them in the case.
		/// </para><para>
		/// If a contact didn't send any messages, they can be included in the list of contacts but Tethr will not do anything with them.
		/// </para>
		/// </summary>
		[Required]
		public List<CaptureCaseContact> Contacts { get; set; } = new();
	}
}