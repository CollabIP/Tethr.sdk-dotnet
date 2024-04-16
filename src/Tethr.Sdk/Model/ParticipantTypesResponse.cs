namespace Tethr.Sdk.Model;

public class ParticipantTypesResponse
{
    public List<ParticipantTypeSummary> Types { get; set; } = new();
}

public class ParticipantTypeSummary
{
    /// <summary>
    /// The identifier for the type of participant (e.g., Agent, Customer, etc.). 
    /// This property categorizes the participant in interactions, facilitating targeted analysis and insights.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// A collection of sources linked to this participant reference. Primarily applicable to Agent types, 
    /// this property lists the origins or systems from which the participant data is sourced, enhancing traceability and data integrity.
    /// </summary>
    public List<string> Sources { get; set; } = new();

    /// <summary>
    /// Flags whether the participant is internal to the organization. An internal participant typically includes company agents or staff, 
    /// aiding in distinguishing between external customer interactions and internal communications for analytics purposes.
    /// </summary>
    public bool IsInternal { get; set; }

    /// <summary>
    /// Indicates whether interactions involving this participant type should be excluded from analytics and script classifiers. 
    /// Setting this to true can be useful for filtering out noise or irrelevant data, focusing analytical resources on meaningful interactions.
    /// </summary>
    public bool IgnoreInAnalytics { get; set; }
}