using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vertical.HubSpot.Api.Extern;

namespace Vertical.HubSpot.Api.Http {

    /// <summary>
    /// http client using build in <see cref="HttpClient"/> for communication
    /// </summary>
    public class SystemHttpClient : IHttpClient {
        readonly HttpClient client=new HttpClient();

        /// <inheritdoc />
        public Uri BaseAddress {
            get => client.BaseAddress;
            set => client.BaseAddress = value;
        }

        /// <inheritdoc />
        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content) {
            return client.PostAsync(url, content);
        }

        /// <inheritdoc />
        public Task<HttpResponseMessage> PatchAsync(string url, HttpContent content) {
            return client.PatchAsync(url, content);
        }

        /// <inheritdoc />
        public Task<HttpResponseMessage> PutAsync(string url, HttpContent content) {
            return client.PutAsync(url, content);
        }

        /// <inheritdoc />
        public Task<HttpResponseMessage> DeleteAsync(string url) {
            return client.DeleteAsync(url);
        }

        /// <inheritdoc />
        public Task<HttpResponseMessage> GetAsync(string url) {
            return client.GetAsync(url);
        }
    }
}