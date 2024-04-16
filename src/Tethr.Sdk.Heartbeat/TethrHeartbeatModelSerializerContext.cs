using System.Text.Json.Serialization;

namespace Tethr.Sdk.Heartbeat;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(MonitorEvent))]
[JsonSerializable(typeof(MonitorStatus))]
public sealed partial class TethrHeartbeatModelSerializerContext : JsonSerializerContext;