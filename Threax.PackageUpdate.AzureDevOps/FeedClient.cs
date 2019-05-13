using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Threax.PackageUpdate.AzureDevOps
{
    /// <summary>
    /// A simple client to read from the package feed in devops. Be sure to set the org property to your org or else this won't work.
    /// </summary>
    public class FeedClient : VssHttpClientBase
    {
        public FeedClient(Uri baseUrl, VssCredentials credentials) : base(baseUrl, credentials)
        {
        }

        public FeedClient(Uri baseUrl, VssCredentials credentials, VssHttpRequestSettings settings) : base(baseUrl, credentials, settings)
        {
        }

        public FeedClient(Uri baseUrl, VssCredentials credentials, params DelegatingHandler[] handlers) : base(baseUrl, credentials, handlers)
        {
        }

        public FeedClient(Uri baseUrl, HttpMessageHandler pipeline, bool disposeHandler) : base(baseUrl, pipeline, disposeHandler)
        {
        }

        public FeedClient(Uri baseUrl, VssCredentials credentials, VssHttpRequestSettings settings, params DelegatingHandler[] handlers) : base(baseUrl, credentials, settings, handlers)
        {
        }
        
        /// <summary>
        /// The org to use when looking up packages.
        /// </summary>
        public String Org { get; set; }

        public async Task<Feeds> GetFeeds()
        {
            CheckOrg();
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"https://feeds.dev.azure.com/{Org}/_apis/packaging/feeds?api-version=5.0-preview.1"))
            {
                return await this.SendAsync<Feeds>(request);
            }
        }

        public async Task<Packages> GetPackages(Guid feedId, bool? includeAllVersions = null, Guid? directUpstreamId = null, bool? includeDescription = null)
        {
            CheckOrg();
            StringBuilder urlBuilder = new StringBuilder($"https://feeds.dev.azure.com/{Org}/_apis/packaging/Feeds/{feedId}/packages?api-version=5.0-preview.1");
            if (includeAllVersions.HasValue)
            {
                urlBuilder.Append($"&includeAllVersions={includeAllVersions}");
            }
            if (directUpstreamId.HasValue)
            {
                urlBuilder.Append($"&directUpstreamId={directUpstreamId}");
            }
            if (includeDescription.HasValue)
            {
                urlBuilder.Append($"&includeDescription={includeDescription}");
            }
            using (var request = new HttpRequestMessage(HttpMethod.Get, urlBuilder.ToString()))
            {
                {
                    return await this.SendAsync<Packages>(request);
                }
            }
        }

        private void CheckOrg()
        {
            if (Org == null)
            {
                throw new InvalidOperationException("Please set the Org property to your org name.");
            }
        }
    }
}
