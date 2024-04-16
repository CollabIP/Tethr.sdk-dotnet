using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Tethr.Sdk.Model;

/// <summary>
/// Metadata to append to interactions. (e.g. Call, Case)
/// </summary>
public abstract class AsyncMetadataInteractionBase : ITethrMetadata
{
	/// <summary>
	/// The time stamp of the event this metadata represents.
	/// </summary>
	/// <remarks>
	/// Async Metadata is applied in sequential orders, so if two events contain the same field the value from the later event will be used.
	/// </remarks>
	public DateTime? EventTime { get; set; }

	/// <summary>
	/// The free-form JSON object that will be appended to the interaction metadata.
	/// </summary>
	/// <remarks>
	/// The metadata is read by Tethr and used classify calls, Examples:
	/// Contact Center the call was answered in, the disposition code entered by the agent,
	/// the skill group / or product the call was related to, the call routing group from the IVR,
	/// an identifier for phone the agent used to make the call.
	/// 
	/// There is really no limit to what can be put in there, as long as it can be converted to a JToken.
	/// </remarks>
	[Required]
	public JsonElement Metadata { get; set; }
}