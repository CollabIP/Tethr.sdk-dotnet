namespace Tethr.Sdk.Model;

public class Segment
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
		/// Category Types represent the sub system with-in Tethr that assigned the Classification to the interaction.
		/// </remarks>
		public string? CategoryType { get; set; }

		/// <summary>
		/// The Category ID from the given sub system in Tethr that assigned the Classification
		/// </summary>
		/// <remarks>
		/// The any given ID will be unique to a given Category Type.
		/// </remarks>
		public string? CategoryId { get; set; }

		/// <summary>
		/// The user-friendly name of the Classification
		/// </summary>
		public string? DisplayName { get; set; }

		/// <summary>
		/// The start time of the segment in Milliseconds from the start of the interaction.
		/// </summary>
		public long StartMs { get; set; }

		/// <summary>
		/// The end time of the segment in Milliseconds from the start of the interaction.
		/// </summary>
		public long EndMs { get; set; }
		
		/// <summary>
		/// The start time of the segment in UTC.
		/// </summary>
		public DateTime UtcStart { get; set; }
		
		/// <summary>
		/// The end time of the segment in UTC.
		/// </summary>
		public DateTime UtcEnd { get; set; }

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
		/// Where appropriate, the content will be part of the transcript that triggered the segments.
		/// </summary>
		public string? Content { get; set; }
		
		/// <summary>
		/// Indicates if the Segment was detected from words on the current interactions, or if it was detected from another interactions that took place at the same time
		/// and then is being projected on to this interaction.
		/// </summary>
		public bool FromFlow { get; set; }
}
