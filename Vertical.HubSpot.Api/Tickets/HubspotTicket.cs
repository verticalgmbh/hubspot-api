using System.Runtime.Serialization;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.Tickets {

    /// <summary>
    /// base data for a ticket in hubspot
    /// </summary>
    public class HubspotTicket : HubspotObject {

        /// <summary>
        /// subject line
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// ticket content text
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The ID of the pipeline the ticket is in
        /// </summary>
        [Name("hs_pipeline")]
        public int Pipeline { get; set; }

        /// <summary>
        /// The ticket's stage in its pipeline
        /// </summary>
        [Name("hs_pipeline_stage")]
        public int Stage { get; set; }
    }
}