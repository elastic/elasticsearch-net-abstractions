using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Managed.Ephemeral.Plugins;

namespace Elastic.Xunit.XunitPlumbing
{
	public class RequiresPluginAttribute : Attribute
	{
		public IList<ElasticsearchPluginConfiguration> Plugins { get; }

		public RequiresPluginAttribute(params ElasticsearchPluginConfiguration[] plugins)
		{
			if (plugins == null)
				throw new ArgumentNullException(nameof(plugins));

			this.Plugins = plugins.ToList();
		}
	}
}
