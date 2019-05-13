using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threax.PackageUpdate;

namespace Threax.PackageUpdate.Npm
{
    public class NpmjsVersionSource : IVersionSource, IDisposable
    {
        private NpmClient npmClient = new NpmClient();

        public void Dispose()
        {
            npmClient.Dispose();
        }

        public async Task<PackageInfo> GetLatestVersion(string packageName)
        {
            var packageInfo = await npmClient.GetPackageInfo(packageName);
            if(packageInfo == null)
            {
                return null;
            }

            return new PackageInfo()
            {
                Name = packageInfo.Name,
                Version = packageInfo.LatestVersion
            };
        }
    }
}
