using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Tethr.Sdk.Model;

namespace Tethr.Sdk.Heartbeat;

public static class TethrHeartbeatExtensions
{
    [UnconditionalSuppressMessage("SingleFile", "IL3000:Avoid accessing Assembly file path when publishing as a single file", Justification = "<Pending>")]
    public static async Task Send(this ITethrHeartbeat heartbeat, MonitorStatus monitorStatus)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var assemblyLocation = assembly.Location;
        var productVersion = string.IsNullOrEmpty(assemblyLocation) 
            ? "0.0.0.0"
            : FileVersionInfo.GetVersionInfo(assemblyLocation).ProductVersion; 

        await heartbeat.Send(new MonitorEvent
        {
            Name = Environment.MachineName,
            Status = monitorStatus,
            TimeStamp = DateTimeOffset.UtcNow,
            SoftwareVersion = productVersion ?? "0.0.0"
        }).ConfigureAwait(false);
    }
}