using Microsoft.Extensions.DependencyInjection;
using Tethr.Sdk.Session;

namespace Tethr.Sdk;

public static class TethrStartupExtensions
{
    public static IServiceCollection AddTethr(this IServiceCollection services, Action<TethrOptions>? configure = null)
    {
        var optionsBuilder = services.AddOptions<TethrOptions>().BindConfiguration("Tethr");
        if (configure is not null) optionsBuilder.Configure(configure);

        services.AddSingleton<ITethrSession, TethrSession>();
        services.AddSingleton<ITethrArchivedRecording, TethrArchivedRecording>();
        services.AddSingleton<ITethrAsyncMetadata, TethrAsyncMetadata>();
        services.AddSingleton<ITethrChat, TethrChat>();
        services.AddSingleton<ITethrHeartbeat, TethrHeartbeat>();
        services.AddSingleton<ITethrRecordingSettings, TethrRecordingSettings>();
        services.AddSingleton<ITethrSessionStatus, TethrSessionStatus>();

        return services;
    }
}