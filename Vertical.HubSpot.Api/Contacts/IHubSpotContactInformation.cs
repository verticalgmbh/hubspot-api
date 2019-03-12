using System;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.Contacts {

    /// <summary>
    /// interface for hubspot predefined properties of <see cref="HubSpotContact"/>s
    /// </summary>
    public interface IHubSpotContactInformation {

        /// <summary>
        /// Annual company revenue
        /// </summary>
        string AnnualRevenue { get; set; }

        /// <summary>
        /// The number of deals associated with this contact. This is set automatically by HubSpot.
        /// </summary>
        [Name("num_associated_deals")]
        long AssociatedDeals { get; set; }


        /// <summary>
        /// A contact's city of residence
        /// </summary>
        string City { get; set; }

        /// <summary>
        /// A company's name
        /// </summary>
        string CompanyName { get; set; }

        /// <summary>
        /// size of company
        /// </summary>
        [Name("company_size")]
        string CompanySize { get; set; }

        /// <summary>
        /// The owner of a contact. This can be any HubSpot user or Salesforce integration user, and can be set manually or via Workflows.
        /// </summary>
        [Name("hubspot_owner_id")]
        long ContactOwner { get; set; }

        /// <summary>
        /// The contact's country of residence. This might be set via import, form, or integration.
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// The date that a contact entered the system
        /// </summary>
        DateTime CreateDate { get; set; }

        /// <summary>
        /// date when contact was born
        /// </summary>
        [Name("date_of_birth")]
        DateTime DateOfBirth { get; set; }

        /// <summary>
        /// The days that elapsed from when a contact was created until they closed as a customer. This is set automatically by HubSpot and can be used for segmentation and reporting.
        /// </summary>
        [Name("days_to_close")]
        string DaysToClose { get; set; }

        [Name("blog_default_hubspot_blog_6420516168_subscription")]
        string DefaultHubspotBlogEmailSubscription { get; set; }

        string Degree { get; set; }

        /// <summary>
        /// Domain to which the registration invitation email for Content Membership was sent to
        /// </summary>
        [Name("hs_content_membership_registration_domain_sent_to")]
        string RegistrationDomain { get; set; }

        /// <summary>
        /// A contact's email address
        /// </summary>
        string EMail { get; set; }

        /// <summary>
        /// Email Confirmation status of user of Content Membership
        /// </summary>
        [Name("hs_content_membership_email_confirmed")]
        bool EMailConfirmed { get; set; }

        /// <summary>
        /// A contact's email address domain
        /// </summary>
        [Name("hs_email_domain")]
        string EMailDomain { get; set; }

        /// <summary>
        /// A contact's primary fax number
        /// </summary>
        string Fax { get; set; }

        [Name("A contact's primary fax number")]
        string FieldOfStudy { get; set; }

        [Name("first_deal_created_date")]
        DateTime FirstDealCreated { get; set; }

        /// <summary>
        /// A contact's first name
        /// </summary>
        string FirstName { get; set; }

        string Gender { get; set; }

        [Name("graduation_date")]
        DateTime GraduationDate { get; set; }

        /// <summary>
        /// The number that shows qualification of contacts to sales readiness. It can be set in HubSpot's Lead Scoring app
        /// </summary>
        int HubSpotScore { get; set; }

        /// <summary>
        /// The team of the owner of a contact
        /// </summary>
        [Name("hubspot_team_id")]
        long HubSpotTeam { get; set; }

        /// <summary>
        /// A company's industry
        /// </summary>
        string Industry { get; set; }

        /// <summary>
        /// A contact's IP Address
        /// </summary>
        string IPAddress { get; set; }

        [Name("job_function")]
        string JobFunction { get; set; }

        /// <summary>
        /// A contact's job title
        /// </summary>
        string JobTitle { get; set; }

        /// <summary>
        /// The last time a note, call, email, meeting, or task was logged for a contact. This is set automatically by HubSpot based on user actions in the contact record
        /// </summary>
        [Name("notes_last_updated")]
        DateTime LastActivityDate { get; set; }

        /// <summary>
        /// The last time a call, email, or meeting was logged for a contact. This is set automatically by HubSpot based on user actions in the contact record
        /// </summary>
        [Name("notes_last_contacted")]
        DateTime LastContacted { get; set; }


    }
}