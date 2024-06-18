namespace Tethr.Sdk.Model;

public class Utterance
{
    /// <summary>
    /// The start time of the utterance in Milliseconds from the start of the Interaction.
    /// </summary>
    public long StartMs { get; set; }

    /// <summary>
    /// The end time of the utterance in Milliseconds from the start of the Interaction.
    /// </summary>
    public long EndMs { get; set; }
		
    /// <summary>
    /// The start time of the utterance in UTC.
    /// </summary>
    public DateTime UtcStart { get; set; }
		
    /// <summary>
    /// The end time of the utterance in UTC.
    /// </summary>
    public DateTime UtcEnd { get; set; }

    /// <summary>
    /// The words spoken during this utterance.
    /// </summary>
    public List<Word> Words { get; set; } = new();

    /// <summary>
    /// Raw text of the utterance
    /// </summary>
    public string? Text { get; set; }
}

public class Word
{
	/// <summary>
	/// The start time of the word in Milliseconds from the start of the Interaction.
	/// </summary>
	/// <remarks>
	/// If Tethr is not provided word timings, Tethr will generate timing based on the length of the word the duration of the utterance.
	/// </remarks>
	public long StartMs { get; set; }

	/// <summary>
	/// The end time of the word in Milliseconds from the start of the Interaction.
	/// </summary>
	/// <remarks>
	/// If Tethr is not provided word timings, Tethr will generate timing based on the length of the word the duration of the utterance.
	/// </remarks>
	public long EndMs { get; set; }

	/// <summary>
	/// The text of the word at this time.
	/// </summary>
	public string? Content { get; set; }

	/// <summary>
	/// The confidence score for the transcription.
	/// </summary>
	public double Confidence { get; set; }
}
