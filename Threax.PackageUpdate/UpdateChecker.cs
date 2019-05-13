using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threax.PackageUpdate
{
    public class UpdateChecker
    {
        private List<IVersionSource> versionSources = new List<IVersionSource>();
        private Dictionary<String, PackageInfo> cachedPackageInfo = new Dictionary<string, PackageInfo>();
        private IVersionComparer versionComparer;

        public UpdateChecker(IVersionComparer versionComparer, params IVersionSource[] versionSources)
        {
            this.versionComparer = versionComparer;
            if (versionSources != null)
            {
                this.versionSources.AddRange(versionSources);
            }
        }

        public void AddVersionSource(IVersionSource versionSource)
        {
            this.versionSources.Add(versionSource);
        }

        public async Task<PackageInfo> GetLatestVersion(String packageName)
        {
            PackageInfo result;
            if(!cachedPackageInfo.TryGetValue(packageName, out result))
            {
                foreach(var source in versionSources)
                {
                    var sourceLatest = await source.GetLatestVersion(packageName);
                    if (sourceLatest != null)
                    {
                        if (result == null)
                        {
                            result = sourceLatest;
                        }
                        else
                        {
                            result = this.versionComparer.GetHighestVersion(result, sourceLatest);
                        }
                    }
                }
                this.cachedPackageInfo.Add(packageName, result); //Add package to cache, adding null will happen if the package cannot be found, but we want to cache that too
            }

            return result;
        }

        public async Task<bool> NeedsUpdate(PackageInfo packageInfo)
        {
            var latest = await GetLatestVersion(packageInfo.Name);
            if(latest == null)
            {
                return false;
            }
            return versionComparer.GetHighestVersion(packageInfo, latest) != packageInfo;
        }
    }
}
