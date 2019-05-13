using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Threax.PackageUpdate.Npm
{
    public class NpmClient : HttpClient
    {
        private String baseUrl;

        public NpmClient()
            :this("https://registry.npmjs.com/")
        {

        }

        public NpmClient(String baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public async Task<NpmPackageInfo> GetPackageInfo(String name)
        {
            var builder = new UriBuilder(baseUrl);
            builder.Path = Path.Combine(builder.Path, name);
            using(var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri))
            {
                using(var response = await this.SendAsync(request))
                {
                    if(!response.IsSuccessStatusCode)
                    {
                        if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            return null;
                        }

                        //If we didn't get a 404 thrown an exception
                        throw new InvalidOperationException($"Could not lookup package {name}. Response code was {response.StatusCode}");
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<NpmPackageInfo>(json);
                }
            }
        }
    }
}
