using System;
using Vertical.HubSpot.Api.Companies;
using Vertical.HubSpot.Api.Models;

namespace Vertical.HubSpot.Api {

    /// <summary>
    /// accesses hubspot using the rest api
    /// </summary>
    /// <remarks>
    /// https://developers.hubspot.com/docs/overview
    /// </remarks>
    public class HubSpot {

        /// <summary>
        /// creates a new <see cref="HubSpot"/>
        /// </summary>
        /// <param name="apikey">key used to access api</param>
        public HubSpot(string apikey) {
            ModelRegistry registry = new ModelRegistry();
            HubSpotRestClient restclient = new HubSpotRestClient(apikey, new Uri("https://api.hubapi.com/"));
            Companies = new CompanyApi(restclient, registry);
        }

        /// <summary>
        /// access to companies
        /// </summary>
        public CompanyApi Companies { get; }
    }
}