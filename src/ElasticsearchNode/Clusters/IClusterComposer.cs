namespace Elastic.Managed.Clusters
{
	public interface IClusterComposer
	{
		void Install();

		void OnBeforeStart();

		void ValidateAfterStart();

		void OnStop();
	}
}
