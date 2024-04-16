namespace Tethr.Sdk.Model;

public class CustomFieldsResponse
{
    public List<CustomFieldResponse> CustomFields { get; set; } = new();
}

public class CustomFieldResponse
{
    /// <summary>
    /// The unique identifier for the Custom Field.
    /// </summary>
    public string? Id { get; set; }
		
    /// <summary>
    /// The display name for the Custom Field.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// The type of the Custom Field.
    /// </summary>
    public CustomFieldType FieldType { get; set; }
		
    /// <summary>
    /// This is used to determine if the custom field is high cardinality.
    /// <para>
    /// By default Tethr limits the number of unique values that can be stored for a custom field.
    /// If a custom field is high cardinality, then this increases the limit,
    /// but also removes the option to use auto-complete with-in the Tethr Filter UI.
    /// </para>
    /// </summary>
    public bool HighCardinality { get; set; }

    /// <summary>
    /// Can be Internal, External, or none. This can be used for targeting Custom Fields from a category for processing.
    /// </summary>
    public List<string> Tags { get; set; } = new();
		
    /// <summary>
    /// The field is marked as deleted.
    /// <para>
    /// The field may still exist on interactions, but can no longer be updated, and will not be evaluated for new interactions or during a re-process.
    /// </para>
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// The order of the Custom Field in the Tethr UI.
    /// </summary>
    public int Order { get; set; }
		
    /// <summary>
    /// If specified, this is the name under which this field will be grouped in the Tethr UI.
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// If true, this field will be hidden in the Tethr Filter UI.
    /// </summary>
    public bool HideInFilter { get; set; }
}