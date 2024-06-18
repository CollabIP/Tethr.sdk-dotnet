using System.Text.Json.Serialization;

namespace Tethr.Sdk.Model;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
#if NET8_0_OR_GREATER
    UseStringEnumConverter = true,
#endif
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(AsyncMetadataSessionRequest))]
[JsonSerializable(typeof(AsyncMetadataMasterRequest))]
[JsonSerializable(typeof(AsyncMetadataCaseRequest))]
[JsonSerializable(typeof(AsyncMetadataCollectionRequest))]

[JsonSerializable(typeof(InteractionDetailsResponse))]
[JsonSerializable(typeof(InteractionShareResponse))]
[JsonSerializable(typeof(InteractionShareRequest))]
[JsonSerializable(typeof(InteractionPurgeRequest))]
[JsonSerializable(typeof(InteractionPurgeByCustomerMetadataRequest))]
[JsonSerializable(typeof(InteractionPurgeSummary))]

[JsonSerializable(typeof(SessionStatusRequest))]
[JsonSerializable(typeof(SessionStatusResponse))]
[JsonSerializable(typeof(SessionExcludeBulkRequest))]
[JsonSerializable(typeof(CaptureChatRequest))]
[JsonSerializable(typeof(CaptureChatContact))]
[JsonSerializable(typeof(CaptureCallRequest))]
[JsonSerializable(typeof(CaptureCaseRequest))]
[JsonSerializable(typeof(CaptureResponse))]
[JsonSerializable(typeof(CaptureStatusHeartbeatRequest))]

[JsonSerializable(typeof(CategoryInfoArrayResponse))]
[JsonSerializable(typeof(CustomFieldsResponse))]
[JsonSerializable(typeof(ParticipantTypesResponse))]

[JsonSerializable(typeof(AppendMetadataBySessionIdRequest))]
[JsonSerializable(typeof(AppendMetadataByMasterIdRequest))]
[JsonSerializable(typeof(AppendMetadataByCaseReferenceIdRequest))]
[JsonSerializable(typeof(AppendMetadataByCollectionIdRequest))]

public sealed partial class TethrModelSerializerContext : JsonSerializerContext;