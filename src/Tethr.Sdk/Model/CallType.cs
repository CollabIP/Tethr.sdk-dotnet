using System.Text.Json.Serialization;

namespace Tethr.Sdk.Model;

/// <summary>
/// Type of call. This is used to determine how to bill customers (voice calls are billed differently from live chat sessions).
/// </summary>
#if !NET8_0_OR_GREATER
[JsonConverter(typeof(JsonStringEnumConverter))]
#endif
public enum CallType
{
    /// <summary>
    /// Phone call
    /// </summary>
    Call,

    /// <summary>
    /// Live chat transcription
    /// </summary>
    Chat,
		
    Flow,
		
    Case
}
