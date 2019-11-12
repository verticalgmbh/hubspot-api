using System;
using Vertical.HubSpot.Api.Associations;
using Vertical.HubSpot.Api.Companies;
using Vertical.HubSpot.Api.Contacts;
using Vertical.HubSpot.Api.Deals;
using Vertical.HubSpot.Api.Models;
using Vertical.HubSpot.Api.Tickets;

namespace Vertical.HubSpot.Api {

    /// <summary>
    /// accesses hubspot using the rest api
    /// </summary>
    /// <remarks>
    /// https://developers.hubspot.com/docs/overview
    /// </remarks>
    public class HubSpot : IHubSpot {

        /// <summary>
        /// creates a new <see cref="HubSpot"/>
        /// </summary>
        /// <remarks>this connects to the default endpoint hubspot provides</remarks>
        /// <param name="apikey">key used to access api</param>
        public HubSpot(string apikey)
            : this(new HubSpotRestClient(apikey, new Uri("https://api.hubapi.com/")))
        {
        }

        /// <summary>
        /// creates a new <see cref="HubSpot"/>
        /// </summary>
        /// <param name="apikey">key used to access api</param>
        /// <param name="endpoint">base endpoint to use for rest calls</param>
        public HubSpot(string apikey, Uri endpoint) 
        : this(new HubSpotRestClient(apikey, endpoint))
        {
        }

        /// <summary>
        /// creates a new <see cref="HubSpot"/>
        /// </summary>
        /// <param name="restclient">rest client used to access hubspot</param>
        public HubSpot(HubSpotRestClient restclient)
        {
            ModelRegistry registry = new ModelRegistry();
            Contacts = new ContactApi(restclient, registry);
            Companies = new CompanyApi(restclient, registry);
            Associations = new AssociationApi(restclient);
            Deals = new DealsApi(restclient, registry);
            Tickets=new TicketsApi(restclient, registry);
        }

        /// <summary>
        /// creates a new <see cref="HubSpot"/> access
        /// </summary>
        /// <param name="contacts">contacts api to use</param>
        /// <param name="companies">companies api to use</param>
        /// <param name="associations">associations api to use</param>
        /// <param name="deals">deals api to use</param>
        /// <param name="tickets">tickets api to use</param>
        public HubSpot(ContactApi contacts, CompanyApi companies, AssociationApi associations, DealsApi deals, ITicketsApi tickets) {
            Contacts = contacts;
            Companies = companies;
            Associations = associations;
            Deals = deals;
            Tickets = tickets;
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

        /// <summary>
        /// access to deals
        /// </summary>
        public DealsApi Deals { get; }

        /// <inheritdoc />
        public ITicketsApi Tickets { get; }
    }
}