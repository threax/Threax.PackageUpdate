using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threax.PackageUpdate.Npm
{
    public class PackageLock
    {
        public static PackageLock FromFile(String path)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return FromStream(stream);
            }
        }

        public static PackageLock FromStream(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                return FromReader(streamReader);
            }
        }

        public static PackageLock FromReader(TextReader streamReader)
        {
            return JsonConvert.DeserializeObject<PackageLock>(streamReader.ReadToEnd());
        }

        public Dictionary<String, Dependency> Dependencies { get; set; } = new Dictionary<string, Dependency>();
    }
}
