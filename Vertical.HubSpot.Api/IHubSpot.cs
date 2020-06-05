using Vertical.HubSpot.Api.Associations;
using Vertical.HubSpot.Api.Companies;
using Vertical.HubSpot.Api.Contacts;
using Vertical.HubSpot.Api.Deals;
using Vertical.HubSpot.Api.Engagements;
using Vertical.HubSpot.Api.Owners;
using Vertical.HubSpot.Api.Tickets;

namespace Vertical.HubSpot.Api {
    /// <summary>
    /// an implementation of hubspot api
    /// </summary>
    public interface IHubSpot {

        /// <summary>
        /// access to contacts
        /// </summary>
        IContactApi Contacts { get; }

        /// <summary>
        /// access to companies
        /// </summary>
        ICompanyApi Companies { get; }

        /// <summary>
        /// access to associations
        /// </summary>
        IAssociationApi Associations { get; }

        /// <summary>
        /// access to deals
        /// </summary>
        IDealsApi Deals { get; }

        /// <summary>
        /// access to ticket information
        /// </summary>
        ITicketsApi Tickets { get; }

        /// <summary>
        /// access to engagements
        /// </summary>
        IEngagementsApi Engagements { get; }

        /// <summary>
        /// access to owners
        /// </summary>
        IOwnersApi Owners { get; }
    }
}