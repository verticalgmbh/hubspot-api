using System;
using Vertical.HubSpot.Api.Contacts;
using Vertical.HubSpot.Api.Http;

namespace Vertical.HubSpot.Api
{
    public class HubSpotOptions
    {
        public string ApiKey { get; set; }
        public Uri ApiUrl { get; set; } = new Uri("https://api.hubapi.com/");

        public HubSpotContactOptions Contact { get; set; } = new HubSpotContactOptions();

        /// <summary>
        /// http client to use
        /// </summary>
        public IHttpClient HttpClient { get; set; }
    }
}