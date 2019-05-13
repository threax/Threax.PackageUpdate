using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threax.PackageUpdate
{
    public interface IVersionComparer
    {
        /// <summary>
        /// Get the PackageInfo with the highest version out of the 2 passed in. The returned PackageInfo must be one
        /// of the ones passed in. If the orig and compare versions are the same, orig will be returned.
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        PackageInfo GetHighestVersion(PackageInfo orig, PackageInfo compare);
    }
}
