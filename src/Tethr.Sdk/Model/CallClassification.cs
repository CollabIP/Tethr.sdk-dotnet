using System.Text.Json.Serialization;

namespace Tethr.Sdk.Model;

public class CallClassification
{
		/// <summary>
		/// A unique ID given to each instance of Classification.
		/// </summary>
		/// <remarks>
		/// This is only used for tracking a given instance in Tethr.
		/// </remarks>
		public string? Id { get; set; }

		/// <summary>
		/// The Category Type of the Classifications
		/// </summary>
		/// <remarks>
		/// Category Types represent the sub system with-in Tethr that assigned the Classification to the Interaction.
		/// </remarks>
		public string? CategoryType { get; set; }

		/// <summary>
		/// The Category ID from the given sub system in Tethr that assigned the Classification
		/// </summary>
		/// <remarks>
		/// The given ID will be unique to a given Category Type.
		/// </remarks>
		public string? CategoryId { get; set; }

		/// <summary>
		/// The user-friendly name of the Classification
		/// </summary>
		public string? DisplayName { get; set; }

		/// <summary>
		/// The start time of the classification in Milliseconds from the start of the Interaction, if applicable.
		/// </summary>
		public long? StartMs { get; set; }

		/// <summary>
		/// The end time of the classification in Milliseconds from the start of the Interaction, if applicable.
		/// </summary>
		public long? EndMs { get; set; }
		
		/// <summary>
		/// The start time of the classification in UTC, if applicable.
		/// </summary>
		public DateTime? UtcStart { get; set; }
		
		/// <summary>
		/// The end time of the classification in UTC, if applicable.
		/// </summary>
		public DateTime? UtcEnd { get; set; }

		/// <summary>
		/// A numerical value for the classification, if applicable.
		/// </summary>
		/// <remarks>
		/// This value will represent a different metric for each Category Type.
		/// </remarks>
		/// <example>
		/// When looking at Text Classification, the value will be a scale from 1 to 0 representing how well it fit the model.
		/// </example>
		public double Value { get; set; }
		
		/// <summary>
		/// The original value, if this classification has been manually overridden.
		/// </summary>
		public double? OrigValue { get; set; }
	
		/// <summary>
		/// Indicates that this value was manually overridden
		/// </summary>	
		public bool IsOverridden { get; set; }

		/// <summary>
		/// Indicates that this classification was required but not hit
		/// </summary>
		public bool Missed { get; set; }

		/// <summary>
		/// Relevant content of the Classification
		/// </summary>
		public string? Content { get; set; }
		
		public bool FromFlow { get; set; }
}