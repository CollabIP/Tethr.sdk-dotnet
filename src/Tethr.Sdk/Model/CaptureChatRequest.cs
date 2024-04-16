using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Tethr.Sdk.Model;

public class CaptureChatRequest : ITethrMetadata
{
	/// <summary>
	/// The SessionId is a unique identifier for each chat.
	/// </summary>
	/// <remarks>
	/// Session ID needs to be unique to any and all recordings uploaded to a given instance of Tethr
	/// 
	/// When not used in conjunction with a MasterId, Session ID should be something 
	/// that can be used by other systems to connect a call in Tethr to the same call in other systems.
	/// 
	/// When a MasterId is provided, the Master ID can be used link a Tethr call to other systems.
	/// But the SessionID should still be something that the originating call recorder can use to lookup
	/// a given recording with.  This allows for the recorder to validate that Tethr did in fact get a call
	/// as part of a regular integrity or verification process.
	/// </remarks>
	[Required]
	public string? SessionId { get; set; }

	/// <summary>
	/// A unique identify that may be shared between one or more chats, that together represent one complete chat.
	/// </summary>
	public string? MasterId { get; set; }

	/// <summary>
	/// The ID to use to create composite cases that do NOT treat the group as a single composite interaction for the purposes of analysis
	/// </summary>
	public string? CaseReferenceId { get; set; }

	/// <summary>
	/// The ID to use to create composite interactions that do NOT treat the group as a single correlated interaction for the purposes of analysis
	/// </summary>
	public string? CollectionId { get; set; }

	/// <summary>
	/// The start time of the Chat session, this value should be in UTC.
	/// </summary>
	public DateTime UtcStart { get; set; }
		
	/// <summary>
	/// The end time of the Chat session, this value should be in UTC.
	/// </summary>
	public DateTime UtcEnd { get; set; }

	/// <summary>
	/// A list of Participants that participated in the chat.
	/// </summary>
	public List<CaptureChatContact> Participants { get; set; } = new();

	/// <summary>
	/// Any extra metadata related to the chat that could be used by Tethr.
	/// </summary>
	/// <remarks>
	/// The metadata is read by Tethr and used classify chats, Examples:
	/// Contact Center the chat was answered in, the disposition code entered by the agent,
	/// the skill group / or product the call was related to, the chat routing information
	/// 
	/// There is really no limit to what can be put in there, as long as it can be converted to JSON.
	/// </remarks>
	public JsonElement Metadata { get; set; }
}