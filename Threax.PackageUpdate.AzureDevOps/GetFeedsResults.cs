using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Threax.PackageUpdate.AzureDevOps
{
    public partial class Feeds
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("value")]
        public Feed[] Value { get; set; }
    }

    public partial class Feed
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
