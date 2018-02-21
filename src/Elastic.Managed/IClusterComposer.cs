namespace Elastic.Managed
{
	public interface IClusterComposer
	{
		void Install();

		void OnBeforeStart();

		void ValidateAfterStart();

		void OnStop();
	}
}
