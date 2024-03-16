using System.Text.Json.Serialization;

namespace Tethr.Sdk.Session;

[JsonSerializable(typeof(TokenResponse))]
internal partial class TokenResponseSerializerContext : JsonSerializerContext;