namespace Tethr.Sdk.Model;

public class CallCustomTextHit
{
		/// <summary>
		/// A unique ID given to each instance of a custom text hit.
		/// </summary>
		/// <remarks>
		/// This is only used for tracking a given instance in Tethr.
		/// </remarks>
		public string? Id { get; set; }

		/// <summary>
		/// The Category Type of the custom text hit.
		/// </summary>
		/// <remarks>
		/// Category Types represent the sub system with-in Tethr that assigned the custom text hit to the Interaction.
		/// </remarks>
		public string? CategoryType { get; set; }

		/// <summary>
		/// The Category ID from the given sub system in Tethr that assigned the custom text hit.
		/// </summary>
		/// <remarks>
		/// The any given ID will be unique to a given Category Type.
		/// </remarks>
		public string? CategoryId { get; set; }
	
		/// <summary>
		/// The ID of the custom field that was hit
		/// </summary>			
		public string? CustomFieldId { get; set; }
	
		/// <summary>
		/// The hash of the custom field value that was hit
		/// </summary>		
		public string? CustomFieldHash { get; set; }
	
		/// <summary>
		/// The start character of the custom text field value that was hit
		/// </summary>	
		public int StartIdx { get; set; }
		
		/// <summary>
		/// The end character of the custom text field value that was hit
		/// </summary>
		public int EndIdx { get; set; }

		/// <summary>
		/// A numerical value for the text hit, if applicable.
		/// </summary>
		/// <remarks>
		/// This value will represent a different metric for each Category Type.
		/// </remarks>
		public double Value { get; set; }

		/// <summary>
		/// The user-friendly name of the custom text hit.
		/// </summary>
		public string? DisplayName { get; set; }

		/// <summary>
		/// Relevant content of the Text Hit
		/// </summary>
		public string? Content { get; set; }
}
