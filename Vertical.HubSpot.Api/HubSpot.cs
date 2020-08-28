using System;
using Vertical.HubSpot.Api.Associations;
using Vertical.HubSpot.Api.BlogPost;
using Vertical.HubSpot.Api.Companies;
using Vertical.HubSpot.Api.Contacts;
using Vertical.HubSpot.Api.Deals;
using Vertical.HubSpot.Api.Engagements;
using Vertical.HubSpot.Api.Http;
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
            : this(new HubSpotOptions(){ApiKey = apikey})
        {
        }

        /// <summary>
        /// creates a new <see cref="HubSpot"/>
        /// </summary>
        /// <param name="apikey">key used to access api</param>
        /// <param name="endpoint">base endpoint to use for rest calls</param>
        public HubSpot(string apikey, Uri endpoint) 
        : this(new HubSpotOptions {ApiKey = apikey, ApiUrl = endpoint})
        {
        }

        /// <summary>
        /// creates a new <see cref="HubSpot"/>
        /// </summary>
        /// <param name="options">options for hubspot api</param>
        public HubSpot(HubSpotOptions options)
        {
            options = options ?? new HubSpotOptions();
            options.HttpClient = options.HttpClient ?? new SystemHttpClient();

            HubSpotRestClient restclient = new HubSpotRestClient(options.ApiKey, options.ApiUrl, options.HttpClient);
            ModelRegistry registry = new ModelRegistry();
            Contacts = new ContactApi(options, restclient, registry);
            Companies = new CompanyApi(restclient, registry);
            Associations = new AssociationApi(restclient);
            Deals = new DealsApi(restclient, registry);
            Tickets = new TicketsApi(restclient, registry);
            BlogPosts = new BlogPostApi(restclient, registry);
            Engagements = new EngagementsApi(options, restclient, registry);
        }

        /// <summary>
        /// creates a new <see cref="HubSpot"/> access
        /// </summary>
        /// <param name="contacts">contacts api to use</param>
        /// <param name="companies">companies api to use</param>
        /// <param name="associations">associations api to use</param>
        /// <param name="deals">deals api to use</param>
        /// <param name="tickets">tickets api to use</param>
        /// <param name="blogposts">blogpost api to use</param>
        public HubSpot(IContactApi contacts, ICompanyApi companies, IAssociationApi associations, IDealsApi deals, ITicketsApi tickets, IBlogPostApi blogposts, IEngagementsApi engagements ) {
            Contacts = contacts;
            Companies = companies;
            Associations = associations;
            Deals = deals;
            Tickets = tickets;
            BlogPosts = blogposts;
            Engagements = engagements;
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

        /// <inheritdoc />
        public ITicketsApi Tickets { get; }

        public IBlogPostApi BlogPosts { get; }

        /// <inheritdoc />
        public IEngagementsApi Engagements { get; }
    }
}