using System;
using Vertical.HubSpot.Api.Associations;
using Vertical.HubSpot.Api.BlogPost;
using Vertical.HubSpot.Api.Companies;
using Vertical.HubSpot.Api.Contacts;
using Vertical.HubSpot.Api.Deals;
using Vertical.HubSpot.Api.Engagements;
using Vertical.HubSpot.Api.Models;
using Vertical.HubSpot.Api.Owners;
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
        public HubSpot(HubSpotOptions options)
            : this(new HubSpotRestClient(options.ApiKey, new Uri(options.ApiUrl)), options)
        {
        }

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
        /// <param name="options">options for hubspot api</param>
        public HubSpot(HubSpotRestClient restclient, HubSpotOptions options = null)
        {
            options = options ?? new HubSpotOptions();
            ModelRegistry registry = new ModelRegistry();
            Contacts = new ContactApi(options, restclient, registry);
            Companies = new CompanyApi(restclient, registry);
            Associations = new AssociationApi(restclient);
            Deals = new DealsApi(restclient, registry);
            Tickets = new TicketsApi(restclient, registry);
            BlogPosts = new BlogPostApi(restclient, registry);
            Engagements = new EngagementsApi(options, restclient, registry);
            Owners = new OwnersApi(restclient, registry);
        }

        /// <summary>
        /// creates a new <see cref="HubSpot"/> access
        /// </summary>
        /// <param name="contacts">contacts api to use</param>
        /// <param name="companies">companies api to use</param>
        /// <param name="associations">associations api to use</param>
        /// <param name="deals">deals api to use</param>
        /// <param name="tickets">tickets api to use</param>
        public HubSpot(IContactApi contacts, ICompanyApi companies, IAssociationApi associations, IDealsApi deals, ITicketsApi tickets, IBlogPostApi blogposts, IEngagementsApi engagements, IOwnersApi owners ) {
            Contacts = contacts;
            Companies = companies;
            Associations = associations;
            Deals = deals;
            Tickets = tickets;
            BlogPosts = blogposts;
            Engagements = engagements;
            Owners = owners;
        }

        /// <summary>
        /// access to contacts
        /// </summary>
        public IContactApi Contacts { get; }

        /// <summary>
        /// access to companies
        /// </summary>
        public ICompanyApi Companies { get; }

        /// <summary>
        /// access to associations
        /// </summary>
        public IAssociationApi Associations { get; }

        /// <summary>
        /// access to deals
        /// </summary>
        public IDealsApi Deals { get; }

        /// <summary>
        /// access to owners
        /// </summary>
        public IOwnersApi Owners { get; }

        /// <inheritdoc />
        public ITicketsApi Tickets { get; }

        public IBlogPostApi BlogPosts { get; }

        /// <inheritdoc />
        public IEngagementsApi Engagements { get; }
    }
}