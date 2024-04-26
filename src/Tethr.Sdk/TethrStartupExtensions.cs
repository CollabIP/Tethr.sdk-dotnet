using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Tethr.Sdk.Session;

namespace Tethr.Sdk;

public static class TethrStartupExtensions
{
#if NET7_0_OR_GREATER
    [RequiresDynamicCode(
        "Binding TethrOptions to configuration values may require generating dynamic code at runtime.")]
#else
    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "If using AOT, the user should use .Net 8 build")]
#endif
    [UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", 
        Justification = "All members are referenced in the TethrOptions class")]
    public static IServiceCollection AddTethr(this IServiceCollection services, Action<TethrOptions>? configure = null)
    {
        var optionsBuilder = services.AddOptions<TethrOptions>().BindConfiguration("Tethr");
        if (configure is not null) optionsBuilder.Configure(configure);

        services.AddSingleton<ITethrSession, TethrSession>();
        services.AddSingleton<TethrCapture>();
        services.AddSingleton<TethrAsyncMetadata>();
        services.AddSingleton<TethrInteraction>();

        return services;
    }
}
