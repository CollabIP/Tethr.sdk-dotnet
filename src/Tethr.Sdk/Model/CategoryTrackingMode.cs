using System.Text.Json.Serialization;

namespace Tethr.Sdk.Model;

#if !NET8_0_OR_GREATER
[JsonConverter(typeof(JsonStringEnumConverter))]
#endif
[Flags]
public enum CategoryTrackingMode
{
    None = 0,
    Hits = 1,
    Misses = 2,
    HitsAndMisses = 3
}
