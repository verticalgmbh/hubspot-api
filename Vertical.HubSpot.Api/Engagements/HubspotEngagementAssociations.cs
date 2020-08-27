using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vertical.HubSpot.Api.Engagements
{
    public class HubSpotEngagementAssociations
    {
        [JsonProperty("contactIds")]
        public List<long> ContactIds { get; set; }

        [JsonProperty("companyIds")]
        public List<long> CompanyIds { get; set; }

        [JsonProperty("dealIds")]
        public List<object> DealIds { get; set; }

        [JsonProperty("ownerIds")]
        public List<object> OwnerIds { get; set; }

        [JsonProperty("workflowIds")]
        public List<object> WorkflowIds { get; set; }

        [JsonProperty("ticketIds")]
        public List<object> TicketIds { get; set; }

        [JsonProperty("contentIds")]
        public List<object> ContentIds { get; set; }

        [JsonProperty("quoteIds")]
        public List<object> QuoteIds { get; set; }
    }
}