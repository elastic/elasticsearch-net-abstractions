// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Differ.Exporters
{
	public class ExporterCollection : KeyedCollection<string, IExporter>
	{
		protected override string GetKeyForItem(IExporter item) => item.Format;

		public ExporterCollection(params IExporter[] exporters)
		{
			if (exporters == null)
				throw new ArgumentNullException(nameof(exporters));

			foreach (var exporter in exporters)
				this.Add(exporter);
		}

		public string SupportedFormats => string.Join(", ", this.Select(e => e.Format));
	}
}
