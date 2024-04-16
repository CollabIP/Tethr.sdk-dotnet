using Microsoft.Extensions.DependencyInjection;

namespace Tethr.Sdk.Heartbeat;

public static class TethrHeartBeatStartupExtensions
{
    /// <summary>
    /// Add the Tethr Heartbeat background service that will automatically send heartbeat to the Tethr server.
    /// </summary>
    public static IServiceCollection AddTethrHeartBeatService(this IServiceCollection services, Action<TethrHeartbeatOptions>? configure = null)
    {
        var optionsBuilder = services.AddOptions<TethrHeartbeatOptions>().BindConfiguration("Tethr");
        if (configure is not null) optionsBuilder.Configure(configure);
        services.AddSingleton<TethrHeartbeat>();
        services.AddHostedService<TethrHeartbeatService>();
        return services;
    }
}