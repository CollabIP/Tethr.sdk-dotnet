namespace Tethr.Sdk.Model;

public class CategoryInfoResponse
{
    /// <summary>
    /// Used to associate this definition with category occurrences.
    /// </summary>
    public string? Key { get; set; }
	
    /// <summary>
    /// The type of category. Also, the prefix of the Key.
    /// The processing phrases api returns PhraseDetection, DeepPhrases returns DeepPhrase, sequences returns SequenceDetection, and scores return BasicScore occurrences.
    /// </summary>
    public string? CategoryType { get; set; }
    public string? DisplayName { get; set; }
    public string? ShortName { get; set; }
    public string? Description { get; set; }

    /// <summary>
    /// Information about how a category score should be shown. Can be used to know what color/label/etc to show for scores in each range.
    /// Note that the transformation is already applied by the server so related fields are just provided for reference.
    /// </summary>
    public ScoreRenderOptions? RenderOptions { get; set; }
    public bool Hidden { get; set; }
    public bool ShowInSummary { get; set; }
    public bool ShowInFilter { get; set; }
    public CategoryResultMode ResultMode { get; set; }
	
    /// <summary>
    /// Some categories are track hits, misses, or both. When misses are tracked an occurrence is created to represent that it didn't hit.
    /// Used for sequences.
    /// </summary>
    public CategoryTrackingMode TrackingMode { get; set; }
    public bool IsLibrary { get; set; }
    public List<string> ExamplePhrases { get; set; } = new();
    public bool EnableRealtime { get; set; }

    /// <summary>
    /// Which type of participants this category runs on.
    /// Internal: any internal participant (e.g. Agent),
    /// External: any external participant (e.g. Customer),
    /// Any: any participant.
    /// </summary>
    public string? ParticipantOptions { get; set; }

    /// <summary>
    /// The category groups this category belongs to.
    /// </summary>
    public List<CategoryGroup> Groups { get; set; } = new();

    public class CategoryGroup
    {
        public string? DisplayName { get; set; }
		
        public string? Id { get; set; }
    }
}