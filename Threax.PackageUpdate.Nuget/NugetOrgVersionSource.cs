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
        PackageMetadataResource packageMetadataResource = null;
        private String url;

        public NugetOrgVersionSource()
            :this("https://api.nuget.org/v3/index.json")
        {

        }

        public NugetOrgVersionSource(String url)
            : this(url, new NullLogger())
        {

        }

        public NugetOrgVersionSource(String url, ILogger logger)
        {
            this.logger = logger;
            this.url = url;
        }

        public async Task<PackageInfo> GetLatestVersion(string packageName)
        {
            //Load the nuget api classes, this will be cached
            if (packageMetadataResource == null)
            {
                var providers = new List<Lazy<INuGetResourceProvider>>();
                providers.AddRange(Repository.Provider.GetCoreV3());  // Add v3 API support
                var packageSource = new PackageSource(this.url);
                var sourceRepository = new NuGet.Protocol.Core.Types.SourceRepository(packageSource, providers);
                packageMetadataResource = await sourceRepository.GetResourceAsync<PackageMetadataResource>();
            }

            //Find metadata for passed in package
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
    }
}
