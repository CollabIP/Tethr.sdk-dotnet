using Tethr.Sdk.Model;
using Tethr.Sdk.Session;

namespace Tethr.Sdk
{
	public interface ITethrHeartbeat
	{
		Task Send(MonitorEvent monitorEvent);
	}

	/// <summary>
	/// Used to send Tethr a heartbeat from a broker.  Allowing you to then monitor if a given broker is able to correctly send data to Tethr at all times.
	/// </summary>
	/// <remarks>
	/// An endpoint can be made available in Tethr to allow you to connect up your own monitoring or alerting system to see that status of a given broker.
	/// This is used to give a high level alert of any possible issue that may prevent a broker from uploading calls to Tethr.
	/// </remarks>
	public class TethrHeartbeat(ITethrSession tethrSession) : ITethrHeartbeat
	{
		public async Task Send(MonitorEvent monitorEvent)
		{
			await tethrSession.PostAsync(@"callCapture/v1/monitor",
				monitorEvent,
				TethrModelSerializerContext.Default.MonitorEvent)
				.ConfigureAwait(false);
		}
	}
}
