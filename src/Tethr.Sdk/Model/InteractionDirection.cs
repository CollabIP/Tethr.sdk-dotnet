using System.Text.Json.Serialization;

namespace Tethr.Sdk.Model;

#if !NET8_0_OR_GREATER
[JsonConverter(typeof(JsonStringEnumConverter))]
#endif
public enum InteractionDirection
{
    /// <summary>
    /// The direction is not known
    /// </summary>
    Unknown,

    /// <summary>
    /// The call originated from outside the telephony system
    /// </summary>
    Inbound,

    /// <summary>
    /// The call originated from something inside the telephony system, and is dialing an outside line.
    /// </summary>
    Outbound,

    /// <summary>
    /// The call originated from something inside the telephony system, and is dialing another device inside the telephony system
    /// </summary>
    Internal
}