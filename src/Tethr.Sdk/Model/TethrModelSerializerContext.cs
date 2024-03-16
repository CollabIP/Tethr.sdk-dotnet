using System.Text.Json.Serialization;

namespace Tethr.Sdk.Model;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(Audio))]
[JsonSerializable(typeof(SessionInteractionMetadata))]
[JsonSerializable(typeof(MasterInteractionMetadata))]
[JsonSerializable(typeof(CaseInteractionMetadata))]
[JsonSerializable(typeof(CallShareResponse))]
[JsonSerializable(typeof(CallShare))]
[JsonSerializable(typeof(InteractionResponse))]
[JsonSerializable(typeof(SessionRequest))]
[JsonSerializable(typeof(SessionStatusResponse))]
[JsonSerializable(typeof(ChatSession))]
[JsonSerializable(typeof(CallShare))]
[JsonSerializable(typeof(CallShareResponse))]
[JsonSerializable(typeof(ChatContact))]
[JsonSerializable(typeof(ChatMessage))]
[JsonSerializable(typeof(Contact))]
[JsonSerializable(typeof(MonitorEvent))]
[JsonSerializable(typeof(MonitorStatus))]
[JsonSerializable(typeof(ArchivedRecordingInfo))]
[JsonSerializable(typeof(RecordingSettingSummary))]
[JsonSerializable(typeof(RecordingSettingSummary))]
[JsonSerializable(typeof(IEnumerable<RecordingSettingSummary>))]
public sealed partial class TethrModelSerializerContext : JsonSerializerContext;