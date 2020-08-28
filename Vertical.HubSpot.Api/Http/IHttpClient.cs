using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vertical.HubSpot.Api.Http {

    /// <summary>
    /// http client used to send requests to a server
    /// </summary>
    public interface IHttpClient {

        /// <summary>
        /// base address to send requests to
        /// </summary>
        Uri BaseAddress { get; set; }

        /// <summary>
        /// sends data using POST
        /// </summary>
        /// <param name="url">url to send data to</param>
        /// <param name="content">content data to send</param>
        /// <returns>response message</returns>
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);

        /// <summary>
        /// sends data using PATCH
        /// </summary>
        /// <param name="url">url to send data to</param>
        /// <param name="content">content data to send</param>
        /// <returns>response message</returns>
        Task<HttpResponseMessage> PatchAsync(string url, HttpContent content);

        /// <summary>
        /// sends data using PUT
        /// </summary>
        /// <param name="url">url to send data to</param>
        /// <param name="content">content data to send</param>
        /// <returns>response message</returns>
        Task<HttpResponseMessage> PutAsync(string url, HttpContent content);

        /// <summary>
        /// sends a DELETE request
        /// </summary>
        /// <param name="url">url to send request to</param>
        /// <returns>response message</returns>
        Task<HttpResponseMessage> DeleteAsync(string url);

        /// <summary>
        /// sends a GET request
        /// </summary>
        /// <param name="url">url to send request to</param>
        /// <returns>response message</returns>
        Task<HttpResponseMessage> GetAsync(string url);
    }
}