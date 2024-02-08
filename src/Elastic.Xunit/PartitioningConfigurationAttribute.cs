// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Elastic.Xunit;

/// <summary>
///     An assembly attribute that specifies the <see cref="PartitioningRunOptions" />
///     for Xunit tests within the assembly.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public class PartitioningConfigurationAttribute : Attribute
{
	private readonly Type _type;

	/// <summary>
	///     Creates a new instance of <see cref="PartitioningConfigurationAttribute" />
	/// </summary>
	/// <param name="type">
	///     A type deriving from <see cref="PartitioningRunOptions" /> that specifies the run options
	/// </param>
	public PartitioningConfigurationAttribute(Type type) => _type = type;

	private TOptions? GetOptions<TOptions>() where TOptions : PartitioningRunOptions, new()
	{
		 var options = Activator.CreateInstance(_type) as TOptions;
		 return options ?? new TOptions();
	}

	public static TOptions GetOptions<TOptions>(Assembly assembly) where TOptions : PartitioningRunOptions, new()
	{
		var options = assembly
			.GetCustomAttributes()
			.OfType<PartitioningConfigurationAttribute?>()
			.FirstOrDefault()
			?.GetOptions<TOptions>()
			?? new TOptions();

		return options;
	}
}
