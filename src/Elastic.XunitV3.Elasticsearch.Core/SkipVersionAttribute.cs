// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Stack.ArtifactsApi;
using Nullean.Xunit.Partitions.v3.Sdk;
using Xunit;
using Xunit.v3;

namespace Elastic.XunitV3.Elasticsearch.Core;

/// <summary>
///     An attribute that marks a test or test class to be skipped for given Elasticsearch versions.
///     Evaluated before each test via xUnit v3's <see cref="BeforeAfterTestAttribute" /> pipeline.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class SkipVersionAttribute : BeforeAfterTestAttribute
{
	public SkipVersionAttribute(string skipVersionRangesSeparatedByComma, string reason)
	{
		Reason = reason;
		Ranges = string.IsNullOrEmpty(skipVersionRangesSeparatedByComma)
			? new List<SemVer.Range>()
			: skipVersionRangesSeparatedByComma.Split(',')
				.Select(r => r.Trim())
				.Where(r => !string.IsNullOrWhiteSpace(r))
				.Select(r => new SemVer.Range(r))
				.ToList();
	}

	/// <summary>
	///     The reason why the test should be skipped.
	/// </summary>
	public string Reason { get; }

	/// <summary>
	///     The version ranges for which the test should be skipped.
	/// </summary>
	public IList<SemVer.Range> Ranges { get; }

	/// <inheritdoc />
	public override void Before(MethodInfo methodUnderTest, IXunitTest test)
	{
		var version = ResolveClusterVersion(methodUnderTest.DeclaringType);
		if (version == null)
			return;

		foreach (var range in Ranges)
		{
			var reason = $"{nameof(SkipVersionAttribute)} has range {range} that {version} satisfies";
			if (!string.IsNullOrWhiteSpace(Reason))
				reason += $": {Reason}";

			Assert.SkipUnless(!version.InRange(range), reason);
		}
	}

	private static ElasticVersion ResolveClusterVersion(Type classType)
	{
		if (classType == null)
			return null;

		// Check IPartitionFixture<T> / IClusterFixture<T> interfaces on the class
		foreach (var iface in classType.GetInterfaces())
		{
			if (!iface.IsGenericType)
				continue;

			var genericDef = iface.GetGenericTypeDefinition();
			if (genericDef != typeof(IPartitionFixture<>) && genericDef != typeof(IClusterFixture<>))
				continue;

			var clusterType = iface.GetGenericArguments()[0];
			if (ElasticsearchCluster<ElasticsearchConfiguration>.ClusterVersions
				.TryGetValue(clusterType, out var version))
				return version;
		}

		// Fallback: check constructor parameters for registered cluster types
		foreach (var ctor in classType.GetConstructors())
		{
			foreach (var param in ctor.GetParameters())
			{
				if (ElasticsearchCluster<ElasticsearchConfiguration>.ClusterVersions
					.TryGetValue(param.ParameterType, out var version))
					return version;
			}
		}

		return null;
	}
}
