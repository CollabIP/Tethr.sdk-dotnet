using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tethr.Sdk.Session;

namespace Tethr.Sdk;

public static class TethrStartupExtensions
{
    /// <summary>
    /// Set up the Tethr SDK with the provided options.
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configure">An (optional) callback to configure the TethrOptions</param>
    /// <param name="httpClientAction">An (optional) callback to do additional configuration on the HttpClient.</param>
    /// <returns>The service collection</returns>
#if NET7_0_OR_GREATER
    [RequiresDynamicCode(
        "Binding TethrOptions to configuration values may require generating dynamic code at runtime.")]
#else
    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "If using AOT, the user should use .Net 8 build")]
#endif
    [UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", 
        Justification = "All members are referenced in the TethrOptions class")]
    public static IServiceCollection AddTethr(this IServiceCollection services, Action<TethrOptions>? configure = null, Action<IHttpClientBuilder>? httpClientAction = null)
    {
        var optionsBuilder = services.AddOptions<TethrOptions>().BindConfiguration("Tethr");
        if (configure is not null) optionsBuilder.Configure(configure);
        
        // Configure the HttpClient
        var serviceProvider = services.BuildServiceProvider();
        var tethrOptions = serviceProvider.GetRequiredService<IOptions<TethrOptions>>().Value;
        var log = serviceProvider.GetRequiredService<ILogger<TethrSession>>();
        var httpClientBuilder = services.AddHttpClient(
            TethrSession.HttpClientName, 
            config => TethrSession.ConfigureHttpClient(config, tethrOptions, log));
        
        // Allow the caller to configure the HttpClient
        httpClientAction?.Invoke(httpClientBuilder);
        
        services.AddSingleton<ITethrSession, TethrSession>();
        services.AddSingleton<TethrCapture>();
        services.AddSingleton<TethrAsyncMetadata>();
        services.AddSingleton<TethrInteraction>();
        services.AddSingleton<TethrProcessing>();

        return services;
    }
}
