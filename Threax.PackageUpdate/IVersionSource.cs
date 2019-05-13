using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threax.PackageUpdate
{
    public interface IVersionSource
    {
        /// <summary>
        /// Get the latest version of a package or null if no package is found with the given name.
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        Task<PackageInfo> GetLatestVersion(string packageName);
    }
}
