using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Threax.PackageUpdate.AzureDevOps
{
    public partial class Packages
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("value")]
        public Package[] Value { get; set; }
    }

    public partial class Package
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("protocolType")]
        public String ProtocolType { get; set; }

        [JsonProperty("versions")]
        public Version[] Versions { get; set; }
    }

    public partial class Version
    {
        [JsonProperty("version")]
        public string VersionVersion { get; set; }

        [JsonProperty("isLatest")]
        public bool IsLatest { get; set; }
    }
}
