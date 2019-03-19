using System;
using Vertical.HubSpot.Api.Associations;
using Vertical.HubSpot.Api.Companies;
using Vertical.HubSpot.Api.Contacts;
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
            Contacts = new ContactApi(restclient, registry);
            Companies = new CompanyApi(restclient, registry);
            Associations = new AssociationApi(restclient);
        }

        /// <summary>
        /// access to contacts
        /// </summary>
        public ContactApi Contacts { get; }

        /// <summary>
        /// access to companies
        /// </summary>
        public CompanyApi Companies { get; }

        /// <summary>
        /// access to associations
        /// </summary>
        public AssociationApi Associations { get; }
    }
}