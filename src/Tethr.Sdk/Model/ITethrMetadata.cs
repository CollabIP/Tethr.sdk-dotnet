using System.Text.Json;

namespace Tethr.Sdk.Model;

public interface ITethrMetadata
{
    /// <summary>
    /// Any extra metadata related to the call that could be used by Tethr.
    /// </summary>
    /// <remarks>
    /// The metadata is read by Tethr and used classify calls, Examples:
    /// Contact Center the call was answered in, the disposition code entered by the agent,
    /// the skill group / or product the call was related to, the call routing group from the IVR,
    /// an identifier for phone the agent used to make the call.
    /// 
    /// There is really no limit to what can be put in there, as long as it can be converted to JSON.
    /// </remarks>
    public JsonElement? Metadata { get; set; }
}