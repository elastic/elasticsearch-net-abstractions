using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Elastic.ProcessManagement
{
	public class ObservableProcessArguments
	{
		public string Binary { get; }
		public IEnumerable<string> Args { get; }

		public ObservableProcessArguments(string binary, IEnumerable<string> args)
			: this(binary, args?.ToArray()) { }

		public ObservableProcessArguments(string binary, params string[] args)
		{
			this.Binary = binary;
			this.Args = args;
		}

		// ReSharper disable UnusedAutoPropertyAccessor.Global
		public Action OnBeforeStop { get; set; }
		public TextReader StandardIn { get; set; }
		public Action<ProcessStartInfo> AlterProcessStartInfo { get; set; }
		public Func<int, bool> ValidExitCodePredicate { get; set; }
		// ReSharper enable UnusedAutoPropertyAccessor.Global

	}
}
