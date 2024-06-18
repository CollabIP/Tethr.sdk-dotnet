using System.Text.Json.Serialization;

namespace Tethr.Sdk.Model;

#if !NET8_0_OR_GREATER
[JsonConverter(typeof(JsonStringEnumConverter))]
#endif
[Flags]
public enum CategoryResultMode
{
    None = 0, // default Classifications
    Classifications = 1,
    Segments = 2,
    ClassificationsAndSegments = 3
}
