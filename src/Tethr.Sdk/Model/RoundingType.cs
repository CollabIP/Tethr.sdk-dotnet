using System.Text.Json.Serialization;

namespace Tethr.Sdk.Model;

#if !NET8_0_OR_GREATER
[JsonConverter(typeof(JsonStringEnumConverter))]
#endif
public enum RoundingType
{
    Nearest,
    Floor,
    Ceiling
}