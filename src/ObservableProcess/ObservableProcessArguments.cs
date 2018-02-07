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

		/// <summary>
		/// By default when we kill the process we wait for its completion with a timeout of `10s`.
		/// By specifying `null` you omit the wait for completion alltogether.
		/// </summary>
		public TimeSpan? WaitForExit { get; set; } = TimeSpan.FromSeconds(10);

		/// <summary>
		/// By default processes are started in a threadpool thread assuming you start multiple from the same thread.
		/// By setting this property to true we do not do this. Know however that when multiple instances of observableprocess
		/// stop at the same time they are all queueing for <see cref="System.Diagnostics.Process.WaitForCompletion()"> which may lead to
		/// unexpected behaviour
		/// </summary>
		public bool NoWrapInThread { get; set; }

		// ReSharper enable UnusedAutoPropertyAccessor.Global

	}
}
