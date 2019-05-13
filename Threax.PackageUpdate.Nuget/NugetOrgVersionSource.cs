using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threax.PackageUpdate.Nuget
{
    public class NugetOrgVersionSource : IVersionSource
    {
        ILogger logger;
        SourceRepository sourceRepository;
        PackageMetadataResource packageMetadataResource;

        public NugetOrgVersionSource()
        {
            //Look up packages on nuget
            logger = new NullLogger();
            var providers = new List<Lazy<INuGetResourceProvider>>();
            providers.AddRange(Repository.Provider.GetCoreV3());  // Add v3 API support
            var packageSource = new PackageSource("https://api.nuget.org/v3/index.json");
            sourceRepository = new NuGet.Protocol.Core.Types.SourceRepository(packageSource, providers);
        }

        public async Task<PackageInfo> GetLatestVersion(string packageName)
        {
            if (packageMetadataResource == null)
            {
                packageMetadataResource = await sourceRepository.GetResourceAsync<PackageMetadataResource>();
            }
            var searchMetadata = await packageMetadataResource.GetMetadataAsync(packageName, false, true, logger, CancellationToken.None);
            if (searchMetadata.Any())
            {
                var last = searchMetadata.Last() as PackageSearchMetadata;
                if (last != null)
                {
                    return new PackageInfo()
                    {
                        Name = last.PackageId,
                        Version = last.Version.ToString()
                    };
                }
            }
            return null;
        }

        class NullLogger : ILogger
        {
            public void LogDebug(string data) { }
            public void LogVerbose(string data) { }
            public void LogInformation(string data) { }
            public void LogMinimal(string data) { }
            public void LogWarning(string data) { }
            public void LogError(string data) { }
            public void LogSummary(string data) { }
            public void LogInformationSummary(string data) { }
            public void LogErrorSummary(string data) { }
        }

        class ConsoleLogger : NuGet.Common.ILogger
        {
            public void LogDebug(string data) => Console.WriteLine($"DEBUG: {data}");
            public void LogVerbose(string data) => Console.WriteLine($"VERBOSE: {data}");
            public void LogInformation(string data) => Console.WriteLine($"INFORMATION: {data}");
            public void LogMinimal(string data) => Console.WriteLine($"MINIMAL: {data}");
            public void LogWarning(string data) => Console.WriteLine($"WARNING: {data}");
            public void LogError(string data) => Console.WriteLine($"ERROR: {data}");
            public void LogSummary(string data) => Console.WriteLine($"SUMMARY: {data}");
            public void LogInformationSummary(string data) => Console.WriteLine($"LogInformationSummary: {data}");
            public void LogErrorSummary(string data) => Console.WriteLine($"LogErrorSummary: {data}");
        }
    }
}
