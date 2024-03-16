using Tethr.Sdk.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tethr.Sdk.Session;

namespace Tethr.Sdk.Tests;

public class TethrArchivedRecordingTests
{
    [Test]
    public async ValueTask RecordingInfoJsonGen()
    {
        var recording = new ArchivedRecordingInfo
        {
            SessionId = "Test",
            StartTime = new DateTime(2024, 1, 1, 4, 23, 0).ToUniversalTime(),
            EndTime = new DateTime(2024, 1, 1, 4, 23, 0).ToUniversalTime(),
            Direction = CallDirection.Unknown,
        };
        
        recording.SetMetadata(new MyMetaDataType { IsTest = true }, MyMetaDataTypeContext.Default.MyMetaDataType);

        var requestContentStream = new MemoryStream();
        await JsonSerializer
            .SerializeAsync(requestContentStream, recording, TethrModelSerializerContext.Default.ArchivedRecordingInfo,
                cancellationToken: default)
            .ConfigureAwait(false);
        requestContentStream.Seek(0, SeekOrigin.Begin);

        await Verify(requestContentStream);
    }
    
    [Test]
    public async ValueTask RecordingInfoJson()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var recording = new ArchivedRecordingInfo
        {
            SessionId = "Test",
            StartTime = new DateTime(2024, 1, 1, 4, 23, 0).ToUniversalTime(),
            EndTime = new DateTime(2024, 1, 1, 4, 23, 0).ToUniversalTime(),
            Direction = CallDirection.Unknown,
            Metadata = JsonSerializer.SerializeToElement(new MyMetaDataType { IsTest = true }, options)
        };
      
        var requestContentStream = new MemoryStream();
        await JsonSerializer
            .SerializeAsync(requestContentStream, recording, TethrModelSerializerContext.Default.ArchivedRecordingInfo,
                cancellationToken: default)
            .ConfigureAwait(false);
        requestContentStream.Seek(0, SeekOrigin.Begin);

        await Verify(requestContentStream);
    }
}

[JsonSerializable(typeof(MyMetaDataType))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
public sealed partial class MyMetaDataTypeContext : JsonSerializerContext;

public class MyMetaDataType
{
    public bool IsTest { get; set; }
}