using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api {

    /// <summary>
    /// rest client used to access hubspot
    /// </summary>
    public class HubSpotRestClient {
        readonly Uri baseaddress;
        readonly string apikey;

        /// <summary>
        /// creates a new <see cref="HubSpotRestClient"/>
        /// </summary>
        /// <param name="apikey">key used to access hubspot</param>
        /// <param name="baseaddress">baseaddress of hubspot api</param>
        public HubSpotRestClient(string apikey, Uri baseaddress) {
            this.apikey = apikey;
            this.baseaddress = baseaddress;
        }

        async void CheckForError(HttpResponseMessage response) {
            if(!response.IsSuccessStatusCode) {
                if (response.Content.Headers.ContentLength == 0)
                    throw new HubSpotException($"{response.StatusCode}: {response.ReasonPhrase}");
                throw new HubSpotException(await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// posts a request to hubspot
        /// </summary>
        /// <param name="url">url to post the request to</param>
        /// <param name="request">request data</param>
        /// <param name="parameters">additional query parameters</param>
        /// <returns>response data</returns>
        public async Task<JObject> Post(string url, JToken request, params Parameter[] parameters) {
            url += $"?hapikey={apikey}";
            if (parameters.Length > 0)
                url += $"&{string.Join("&", parameters.Select(p => p.Key + "=" + HttpUtility.UrlEncode(p.Value)))}";

            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = baseaddress;
                HttpResponseMessage response=await client.PostAsync(url, new StringContent(request.ToString(), Encoding.UTF8, "application/json"));
                CheckForError(response);
                if(response.Content.Headers.ContentLength>0)
                    return JObject.Parse(await response.Content.ReadAsStringAsync());
                return null;
            }
        }

        /// <summary>
        /// put a request to hubspot
        /// </summary>
        /// <param name="url">url to post the request to</param>
        /// <param name="request">request data</param>
        /// <param name="parameters">additional query parameters</param>
        /// <returns>response data</returns>
        public async Task<JObject> Put(string url, JToken request, params Parameter[] parameters)
        {
            url += $"?hapikey={apikey}";
            if (parameters.Length > 0)
                url += $"&{string.Join("&", parameters.Select(p => p.Key + "=" + HttpUtility.UrlEncode(p.Value)))}";

            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = baseaddress;
                HttpResponseMessage response = await client.PutAsync(url, new StringContent(request.ToString(), Encoding.UTF8, "application/json"));
                CheckForError(response);

                if (response.Content.Headers.ContentLength > 0)
                    return JObject.Parse(await response.Content.ReadAsStringAsync());
                return null;
            }
        }

        /// <summary>
        /// posts a request to hubspot
        /// </summary>
        /// <param name="url">url to post the request to</param>
        /// <returns>response data</returns>
        public async Task<JObject> Delete(string url)
        {
            url += $"?hapikey={apikey}";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseaddress;
                HttpResponseMessage response = await client.DeleteAsync(url);
                CheckForError(response);
                return JObject.Parse(await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// posts a request to hubspot
        /// </summary>
        /// <param name="url">url to post the request to</param>
        /// <param name="parameters">additional query parameters</param>
        /// <returns>response data</returns>
        public async Task<JObject> Get(string url, params Parameter[] parameters) {
            url += $"?hapikey={apikey}";
            if (parameters.Length > 0)
                url += $"&{string.Join("&", parameters.Select(p => p.Key + "=" + HttpUtility.UrlEncode(p.Value)))}";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseaddress;
                HttpResponseMessage response = await client.GetAsync(url);
                CheckForError(response);
                return JObject.Parse(await response.Content.ReadAsStringAsync());
            }
        }
    }
}