// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Text;
using System.Threading.Tasks;
using TUnit.Core;

namespace Elastic.TUnit.Elasticsearch.Core;

/// <summary>
///     A <see cref="TextWriter" /> that dynamically routes output to the current
///     TUnit test's output via <see cref="TestContext.Current" />.
///     When no test context is active, output is silently discarded.
///     <para>
///         This allows a cached/shared client to route per-request diagnostics
///         to whichever test is currently executing — similar to how TUnit's
///         ASP.NET integration routes server-side logs per-test.
///     </para>
/// </summary>
internal sealed class TUnitTestOutputWriter : TextWriter
{
	public static TUnitTestOutputWriter Instance { get; } = new();

	public override Encoding Encoding => Encoding.UTF8;

	public override void Write(char value) =>
		TestContext.Current?.OutputWriter?.Write(value);

	public override void Write(string value) =>
		TestContext.Current?.OutputWriter?.Write(value);

	public override void WriteLine(string value) =>
		TestContext.Current?.OutputWriter?.WriteLine(value);

	public override void WriteLine() =>
		TestContext.Current?.OutputWriter?.WriteLine();

	public override Task WriteAsync(string value) =>
		TestContext.Current?.OutputWriter?.WriteAsync(value) ?? Task.CompletedTask;

	public override Task WriteLineAsync(string value) =>
		TestContext.Current?.OutputWriter?.WriteLineAsync(value) ?? Task.CompletedTask;
}
