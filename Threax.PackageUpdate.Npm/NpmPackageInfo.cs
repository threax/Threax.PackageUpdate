using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.PackageUpdate.Npm
{
    public class NpmPackageInfo
    {
        public String Name { get; set; }

        [JsonProperty("dist-tags")]
        public Dictionary<String, String> DistTags { get; set; }

        public String LatestVersion
        {
            get
            {
                if (DistTags.TryGetValue("latest", out var latest))
                {
                    return latest;
                }
                return "0.0.0";
            }
        }
    }
}
