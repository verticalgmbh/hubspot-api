using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Extensions;
using Newtonsoft.Json.Linq;
using Vertical.HubSpot.Api.Data;
using Vertical.HubSpot.Api.Logging;

namespace Vertical.HubSpot.Api {

    /// <summary>
    /// rest client used to access hubspot
    /// </summary>
    public class HubSpotRestClient {
        readonly string apikey;
        readonly HttpClient client = new HttpClient();

        /// <summary>
        /// creates a new <see cref="HubSpotRestClient"/>
        /// </summary>
        /// <param name="apikey">key used to access hubspot</param>
        /// <param name="baseaddress">baseaddress of hubspot api</param>
        public HubSpotRestClient(string apikey, Uri baseaddress) {
            this.apikey = apikey;
            client.BaseAddress = baseaddress;
        }

        async Task CheckForError(JToken request, HttpResponseMessage response) {
            if(!response.IsSuccessStatusCode) {
                if (response.Content.Headers.ContentLength == 0) {
                    Logger.Error(this, $"{response.StatusCode}: {response.ReasonPhrase}");
                    throw new HubSpotException($"{response.StatusCode}: {response.ReasonPhrase}", request);
                }

                string errorstring = await response.Content.ReadAsStringAsync();
                Logger.Error(this, errorstring);
                throw new HubSpotException(errorstring, request);
            }
        }

        async Task<T> ReadResponse<T>(HttpResponseMessage message)
        where T : JToken
        {
            if (message.Content.Headers.ContentLength > 0)
            {
                string responsestring = await message.Content.ReadAsStringAsync();
                Logger.Info(this, "Response", responsestring);
                JToken response=JToken.Parse(responsestring);
                if(!(response is T))
                    throw new InvalidOperationException($"Response is not {nameof(T)}");
                return (T) response;
            }

            Logger.Info(this, "No response body");
            return null;
        }

        /// <summary>
        /// posts a request to hubspot
        /// </summary>
        /// <param name="url">url to post the request to</param>
        /// <param name="request">request data</param>
        /// <param name="parameters">additional query parameters</param>
        /// <returns>response data</returns>
        public async Task<T> Post<T>(string url, JToken request, params Parameter[] parameters)
            where T : JToken {
            url += $"?hapikey={apikey}";
            if (parameters.Length > 0)
                url += $"&{string.Join("&", parameters.Select(p => p.Key + "=" + HttpUtility.UrlEncode(p.Value)))}";

            HttpResponseMessage response = await client.PostAsync(url, new StringContent(request.ToString(), Encoding.UTF8, "application/json"));
            using (response) {
                await CheckForError(request, response);
                return await ReadResponse<T>(response);
            }
        }

        /// <summary>
        /// Sends a patch request to hubspot
        /// </summary>
        /// <param name="url">url to post the request to</param>
        /// <param name="request">request data</param>
        /// <param name="parameters">additional query parameters</param>
        /// <returns>response data</returns>
        public async Task<T> Patch<T>(string url, JToken request, params Parameter[] parameters)
            where T : JToken {
            url += $"?hapikey={apikey}";
            if (parameters.Length > 0)
                url += $"&{string.Join("&", parameters.Select(p => p.Key + "=" + HttpUtility.UrlEncode(p.Value)))}";

            HttpResponseMessage response = await client.PatchAsync(url, new StringContent(request.ToString(), Encoding.UTF8, "application/json"));
            using (response) {
                await CheckForError(request, response);
                return await ReadResponse<T>(response);
            }
        }

        /// <summary>
        /// put a request to hubspot
        /// </summary>
        /// <param name="url">url to post the request to</param>
        /// <param name="request">request data</param>
        /// <param name="parameters">additional query parameters</param>
        /// <returns>response data</returns>
        public async Task<T> Put<T>(string url, JToken request, params Parameter[] parameters)
            where T : JToken {
            url += $"?hapikey={apikey}";
            if (parameters.Length > 0)
                url += $"&{string.Join("&", parameters.Select(p => p.Key + "=" + HttpUtility.UrlEncode(p.Value)))}";

            HttpResponseMessage response = await client.PutAsync(url, new StringContent(request.ToString(), Encoding.UTF8, "application/json"));
            using (response) {
                await CheckForError(request, response);
                return await ReadResponse<T>(response);
            }
        }

        /// <summary>
        /// posts a request to hubspot
        /// </summary>
        /// <param name="url">url to post the request to</param>
        /// <returns>response data</returns>
        public async Task<T> Delete<T>(string url)
            where T : JToken {
            url += $"?hapikey={apikey}";
            HttpResponseMessage response = await client.DeleteAsync(url);
            using (response) {
                await CheckForError(null, response);
                return await ReadResponse<T>(response);
            }
        }

        /// <summary>
        /// posts a request to hubspot
        /// </summary>
        /// <param name="url">url to post the request to</param>
        /// <param name="parameters">additional query parameters</param>
        /// <returns>response data</returns>
        public async Task<T> Get<T>(string url, params Parameter[] parameters)
            where T : JToken {
            url += $"?hapikey={apikey}";
            if (parameters.Length > 0)
                url += $"&{string.Join("&", parameters.Select(p => p.Key + "=" + HttpUtility.UrlEncode(p.Value)))}";

            HttpResponseMessage response = await client.GetAsync(url);
            using (response) {
                await CheckForError(null, response);
                return await ReadResponse<T>(response);
            }
        }
    }
}