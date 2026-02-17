// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Stack.ArtifactsApi;
using TUnit.Core;

namespace Elastic.TUnit.Elasticsearch.Core;

/// <summary>
///     A global TUnit hook that evaluates <see cref="SkipVersionAttribute" /> and
///     <see cref="SkipTestAttribute" /> before each test, skipping tests that match.
/// </summary>
public class ElasticsearchTestHooks
{
	[BeforeEvery(Test)]
	public static void EvaluateSkipConditions(TestContext context)
	{
		var testDetails = context.Metadata.TestDetails;
		var allAttributes = GetAllAttributes(testDetails);

		// Evaluate SkipTestAttribute subclasses
		var skipTest = allAttributes
			.OfType<SkipTestAttribute>()
			.FirstOrDefault(a => a.Skip);

		if (skipTest != null)
		{
			Skip.Test(skipTest.Reason);
			return;
		}

		// Resolve the cluster version for version-skip evaluation
		var version = ResolveClusterVersion(testDetails);
		if (version == null)
			return;

		// Evaluate SkipVersionAttribute
		var skipVersionAttributes = allAttributes.OfType<SkipVersionAttribute>();

		foreach (var skipVersion in skipVersionAttributes)
		{
			foreach (var range in skipVersion.Ranges)
			{
				if (!version.InRange(range))
					continue;

				var reason = $"{nameof(SkipVersionAttribute)} has range {range} that {version} satisfies";
				if (!string.IsNullOrWhiteSpace(skipVersion.Reason))
					reason += $": {skipVersion.Reason}";
				Skip.Test(reason);
				return;
			}
		}
	}

	private static IEnumerable<Attribute> GetAllAttributes(TestDetails testDetails)
	{
		foreach (var kvp in testDetails.AttributesByType)
		{
			foreach (var attr in kvp.Value)
				yield return attr;
		}
	}

	private static ElasticVersion ResolveClusterVersion(TestDetails testDetails)
	{
		var classType = testDetails.ClassType;

		// Check constructor parameters for cluster types registered during InitializeAsync
		foreach (var ctor in classType.GetConstructors())
		{
			foreach (var param in ctor.GetParameters())
			{
				if (ElasticsearchCluster<ElasticsearchConfiguration>.ClusterVersions
					.TryGetValue(param.ParameterType, out var version))
					return version;
			}
		}

		// Check ClassDataSource attributes on the class for the generic type argument
		foreach (var attr in classType.GetCustomAttributes(true))
		{
			var attrType = attr.GetType();
			if (!attrType.IsGenericType)
				continue;

			foreach (var arg in attrType.GetGenericArguments())
			{
				if (ElasticsearchCluster<ElasticsearchConfiguration>.ClusterVersions
					.TryGetValue(arg, out var version))
					return version;
			}
		}

		return null;
	}
}
