using System;
using System.Runtime.InteropServices;

namespace Elastic.Stack.Artifacts.Platform
{
	internal static class OsMonikers
	{
		public static readonly string Windows = "windows";
		public static readonly string Linux = "linux";
		public static readonly string OSX = "darwin";

		public static string From(OSPlatform platform)
		{
			if (platform == OSPlatform.Windows) return Windows;
			if (platform == OSPlatform.Linux) return Linux;
			if (platform == OSPlatform.OSX) return OSX;
			return "unknown";
		}

		public static OSPlatform CurrentPlatform()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return OSPlatform.Windows;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return OSPlatform.OSX;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return OSPlatform.Linux;
			throw new Exception($"{RuntimeInformation.OSDescription} is currently not supported please open an issue @elastic/elasticsearch-net-abstractions");
		}


		public static string CurrentPlatformArchiveExtension()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return "zip";
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return "tar.gz";
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return "tar.gz";

			throw new Exception($"{RuntimeInformation.OSDescription} is currently not supported please open an issue @elastic/elasticsearch-net-abstractions");
		}

		public static string CurrentPlatformPackageSuffix()
		{
			var intelX86Suffix = "x86_64";
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return $"{Windows}-{intelX86Suffix}";
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return $"{OSX}-{intelX86Suffix}";
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return $"{Linux}-{intelX86Suffix}";

			throw new Exception($"{RuntimeInformation.OSDescription} is currently not supported please open an issue @elastic/elasticsearch-net-abstractions");
		}

	}
}
