using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vertical.HubSpot.Api.Engagements
{
    public class HubSpotEngagementResult
    {
        [JsonProperty("engagement")]
        public HubspotEngagement Engagement { get; set; }

        [JsonProperty("associations")]
        public HubSpotEngagementAssociations Associations { get; set; }

        [JsonProperty("attachments")]
        public List<object> Attachments { get; set; }

        [JsonProperty("scheduledTasks")]
        public List<HubspotEngagementScheduledTask> ScheduledTasks { get; set; }

        [JsonProperty("metadata")]
        public HubspotEngagementMetadata Metadata { get; set; }
    }
}