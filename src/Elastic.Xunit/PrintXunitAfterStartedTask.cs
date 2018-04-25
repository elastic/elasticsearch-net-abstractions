using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Tasks;

namespace Elastic.Xunit
{
	public class PrintXunitAfterStartedTask : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var name = cluster.GetType().Name;
			cluster.Writer.WriteDiagnostic($"All good! kicking off [{name}] integration tests now");
		}
	}
}
