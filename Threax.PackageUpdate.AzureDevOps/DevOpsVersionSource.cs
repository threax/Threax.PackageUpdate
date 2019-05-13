using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threax.PackageUpdate;

namespace Threax.PackageUpdate.AzureDevOps
{
    /// <summary>
    /// A version source to get package info from an Azure Devops Package Feed.
    /// </summary>
    public class DevOpsVersionSource : IVersionSource
    {
        public const String NuGet = "NuGet";
        public const String Npm = "Npm";

        private Dictionary<String, PackageInfo> packageInfos;

        public DevOpsVersionSource(String protocol, IEnumerable<Package> packages)
        {
            packageInfos = packages.Where(i => i.ProtocolType == protocol)
                .Select(i => new PackageInfo()
                {
                    Name = i.Name,
                    Version = i.Versions.Where(j => j.IsLatest).FirstOrDefault()?.VersionVersion
                }).ToDictionary(i => i.Name);
        }

        public Task<PackageInfo> GetLatestVersion(string packageName)
        {
            if(packageInfos.TryGetValue(packageName, out var value))
            {
                return Task.FromResult(value);
            }
            return Task.FromResult(default(PackageInfo));
        }

        public IEnumerable<PackageInfo> Packages => packageInfos.Values;
    }
}
