using Tethr.Sdk;
using Tethr.Sdk.Model;

namespace Tethr.Demo.Uploader;

public class UploadCallWorker(TethrCapture tethrCapture, ILogger<UploadCallWorker> logger) 
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var callRequest = new CaptureCallRequest
        {
            Direction = InteractionDirection.Inbound,
            SessionId = GenerateSessionId(),
            Participants =
            [
                new()
                {
                    Channel = 0,
                    Type = "Agent",
                    ReferenceId = "agent@example.com"
                },
                new ()
                {
                    Channel = 1,
                    Type = "Customer"
                }
            ],
            NumberDialed = "555-555-5555",
            UtcStart = DateTime.UtcNow,
            Audio = new Audio
            {
                Format = "wav"
            }
        };

        var response = await tethrCapture
            .UploadAsync(callRequest, "sample.wav", "audio/wav", cancellationToken: stoppingToken);
        logger.LogInformation("Uploaded call with session Id {SessionId}, Tethr returned Id {TethrId}",
            callRequest.SessionId, response.Id);
        
        // this demo will wait for the integration processing to complete,
        // however this could take a few hours when loading large batches,
        // so this is why we recommend using the Tethr Outbound webhook if you
        // have additional processing you need to do with the Tethr insights,
        // or to have an out-of-band process check the status periodically.

        while (!stoppingToken.IsCancellationRequested)
        {
            var status = await tethrCapture.GetSessionStatusAsync(callRequest.SessionId, stoppingToken);
            if (status.Status == SessionStatuses.Complete)
            {
                logger.LogInformation("Session {SessionId} processing has Complete", callRequest.SessionId);
                break;
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
    
    private static string GenerateSessionId()
    {
        return "SDKUploader" + DateTime.Now.ToString("yyyyMMddHHmmss");
    }
}
