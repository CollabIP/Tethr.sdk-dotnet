namespace Tethr.Demo.FolderUploader;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);
            }

            await Task.Delay(5000, stoppingToken);
        }
    }
}