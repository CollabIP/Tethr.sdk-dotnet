using System.ComponentModel.DataAnnotations;

namespace Tethr.Sdk.Model;

public class CaptureChatMessage
{
	/// <summary>
	/// The chat message text.
	/// </summary>
	[Required]
	public string? Content { get; set; }

	/// <summary>
	/// The timestamp the chat message was sent
	/// </summary>
	public DateTime UtcTimestamp { get; set; }

	public List<CaptureChatCustomEvent> CustomEvents { get; set; } = new();
}