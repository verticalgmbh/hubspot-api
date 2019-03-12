using System;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.Contacts {

    /// <summary>
    /// interface for contact information about it's lifecycle
    /// </summary>
    public interface IContactLifecycleInformation {

        /// <summary>
        /// The date when a contact's lifecycle stage changed to Customer. This is automatically set by HubSpot for each contact.
        /// </summary>
        [Name("hs_lifecyclestage_customer_date")]
        DateTime BecameCustomer { get; set; }

        /// <summary>
        /// The date when a contact's lifecycle stage changed to Lead. This is automatically set by HubSpot for each contact.
        /// </summary>
        [Name("hs_lifecyclestage_lead_date")]
        DateTime BecameLead { get; set; }

        /// <summary>
        /// The date when a contact's lifecycle stage changed to MQL. This is automatically set by HubSpot for each contact.
        /// </summary>
        [Name("hs_lifecyclestage_marketingqualifiedlead_date")]
        DateTime BecameMarketingQualifiedLead { get; set; }

        /// <summary>
        /// The date when a contact's lifecycle stage changed to SQL. This is automatically set by HubSpot for each contact.
        /// </summary>
        [Name("hs_lifecyclestage_salesqualifiedlead_date")]
        DateTime BecameSalesQualifiedLead { get; set; }

        /// <summary>
        /// The date when a contact's lifecycle stage changed to Subscriber. This is automatically set by HubSpot for each contact.
        /// </summary>
        [Name("hs_lifecyclestage_subscriber_date")]
        DateTime BecameSubscriber { get; set; }

        /// <summary>
        /// The date when a contact's lifecycle stage changed to Evangelist. This is automatically set by HubSpot for each contact.
        /// </summary>
        [Name("hs_lifecyclestage_evangelist_date")]
        DateTime BecameEvangelist { get; set; }

        /// <summary>
        /// The date when a contact's lifecycle stage changed to Opportunity. This is automatically set by HubSpot for each contact.
        /// </summary>
        [Name("hs_lifecyclestage_opportunity_date")]
        DateTime BecameOpportunity { get; set; }

        /// <summary>
        /// The date when a contact's lifecycle stage changed to Other. This is automatically set by HubSpot for each contact.
        /// </summary>
        [Name("hs_lifecyclestage_other_date")]
        DateTime BecameOther { get; set; }

        /// <summary>
        /// The date that a contact became a customer. This property is set automatically by HubSpot when a deal or opportunity is marked as closed-won. It can also be set manually or programmatically.
        /// </summary>
        DateTime CloseDate { get; set; }
    }
}