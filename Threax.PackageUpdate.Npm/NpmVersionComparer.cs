using Semver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threax.PackageUpdate.Npm
{
    public class NpmVersionComparer : IVersionComparer
    {
        public PackageInfo GetHighestVersion(PackageInfo orig, PackageInfo compare)
        {
            var origVersion = SemVersion.Parse(orig.Version);
            var compareVersion = SemVersion.Parse(compare.Version);

            if (origVersion >= compareVersion)
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
