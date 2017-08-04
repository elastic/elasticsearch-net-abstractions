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
			: this(binary, args?.ToArray())
		{
		}

		public ObservableProcessArguments(string binary, params string[] args)
		{
			this.Binary = binary;
			this.Args = args;
		}

		// ReSharper disable UnusedAutoPropertyAccessor.Global

		/// <summary>
		/// Provide a method that will be called with <see cref="ProcessStartInfo"/> before its set on <see cref="Process.StartInfo"/>
		/// </summary>
		public Action<ProcessStartInfo> AlterProcessStartInfo { get; set; }

		public TimeSpan? WaitForExit { get; set; } = TimeSpan.FromSeconds(10);

		// ReSharper enable UnusedAutoPropertyAccessor.Global

	}
}
