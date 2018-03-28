using Elastic.Managed.Ephemeral.Plugins;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;

[assembly: Xunit.TestFrameworkAttribute("Elastic.Xunit.Sdk.ElasticTestFramework", "Elastic.Xunit")]
namespace Elastic.Xunit.Example
{
	public class ReadOnlyCluster : XunitClusterBase
	{
		/// <summary> Called when the cluster is up and running once, useful to bootstrap the cluster </summary>
		protected override void SeedCluster()
		{
			var info = this.Client.RootNodeInfo();
		}
	}

	public class XPackCluster : XunitClusterBase
	{
		public override ElasticsearchPluginConfiguration[] RequiredPlugins { get; } = new[]
		{
			new ElasticsearchPluginConfiguration(ElasticsearchPlugin.XPack),
		};

		/// <summary> Called when the cluster is up and running once, useful to bootstrap the cluster </summary>
		protected override void SeedCluster()
		{
			var info = this.Client.RootNodeInfo();
		}
	}

	public class MyTestClass : ClusterTestClassBase<ReadOnlyCluster>
	{
		public MyTestClass(ReadOnlyCluster cluster) : base(cluster) { }

		[I] public void SomeTest()
		{
			var info = this.Client.RootNodeInfo();

			info.IsValid.Should().BeTrue();
		}
	}
}
