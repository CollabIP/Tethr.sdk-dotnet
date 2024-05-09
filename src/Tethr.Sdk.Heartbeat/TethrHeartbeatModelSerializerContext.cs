using System.Text.Json.Serialization;

namespace Tethr.Sdk.Heartbeat;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
#if NET8_0_OR_GREATER
    UseStringEnumConverter = true,
#endif
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(MonitorEvent))]
[JsonSerializable(typeof(MonitorStatus))]
public sealed partial class TethrHeartbeatModelSerializerContext : JsonSerializerContext;