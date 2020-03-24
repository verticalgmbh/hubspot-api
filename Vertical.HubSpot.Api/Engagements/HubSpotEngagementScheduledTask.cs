using Newtonsoft.Json;

namespace Vertical.HubSpot.Api.Engagements
{
    public class HubspotEngagementScheduledTask
    {
        [JsonProperty("engagementId")]
        public long EngagementId { get; set; }

        [JsonProperty("portalId")]
        public long PortalId { get; set; }

        [JsonProperty("engagementType")]
        public string EngagementType { get; set; }

        [JsonProperty("taskType")]
        public string TaskType { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }
}