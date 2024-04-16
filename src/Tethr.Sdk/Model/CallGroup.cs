namespace Tethr.Sdk.Model;

public class CallGroup
{
    /// <summary>
    /// The ID of the Interaction group in tethr.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The display name of the Interaction group.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// The type of Interaction group (e.g. Division, Location, Team, Skill, Campaign)
    /// </summary>
    /// <example>
    /// Division, Location, Team, Skill, Campaign
    /// </example>
    public string? Type { get; set; }
}