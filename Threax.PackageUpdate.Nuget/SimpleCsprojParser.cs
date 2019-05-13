using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Threax.PackageUpdate.Nuget
{
    /// <summary>
    /// This class will do a simple parse of a csproj file using linq2xml.
    /// </summary>
    ///<remarks>
    /// It would be better to not need this and instead use the Project class from Microsoft.Build.However,
    /// that library will fail to parse the csproj files when running under.net core with the error:
    /// Microsoft.Build Microsoft.Build.Exceptions.InvalidProjectFileException: 'The SDK 'Microsoft.NET.Sdk.Web' specified could not be found.
    /// There are fixes but all of them are complicated and unreliable.This also looks like you have to have the sdk
    /// installed on the target machine and that may not be the case where this app is deployed.
    /// Csproj files are xml files, so just use linq2xml to read the package names. That's all this is trying to do anyway.
    /// </remarks>
    public class SimpleCsprojParser
    {
        private String projectPath;
        private XElement root;

        public SimpleCsprojParser(String projectPath)
        {
            this.projectPath = projectPath;
            using (var reader = new StreamReader(File.Open(projectPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                root = XElement.Load(reader);
            }
        }

        public IEnumerable<PackageInfo> PackageInfo
        {
            get
            {
                foreach (var packageRef in root.Descendants("PackageReference"))
                {
                    var include = packageRef.Attribute("Include")?.Value;
                    var version = packageRef.Attribute("Version")?.Value;
                    if (include != null && version != null)
                    {
                        yield return new PackageInfo()
                        {
                            Name = include,
                            Version = version
                        };
                    }
                }
            }
        }
    }
}
