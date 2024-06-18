using System.ComponentModel.DataAnnotations;

namespace Tethr.Sdk.Model;

public class InteractionShareRequest
{
	/// <summary>
	/// Email address the share token will be for.
	/// </summary>
	[Required]
	public string? Email { get; set; }
		
	/// <summary>
	/// The Tethr Interaction Id the share token will be for.
	/// </summary>
	[Required]
	public string? InteractionId { get; set; }
}