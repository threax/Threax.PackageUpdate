using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threax.PackageUpdate.Nuget
{
    public class NugetVersionComparer : IVersionComparer
    {
        public PackageInfo GetHighestVersion(PackageInfo orig, PackageInfo compare)
        {
            var origVersion = NuGetVersion.Parse(orig.Version);
            var compareVersion = NuGetVersion.Parse(compare.Version);

            if(origVersion >= compareVersion)
            {
                return orig;
            }
            else
            {
                return compare;
            }
        }
    }
}
