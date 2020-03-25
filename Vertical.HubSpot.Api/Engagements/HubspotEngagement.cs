using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vertical.HubSpot.Api.Engagements
{
    public class HubspotEngagement
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("portalId")]
        public long PortalId { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }

        [JsonProperty("lastUpdated")]
        public long LastUpdated { get; set; }

        [JsonProperty("createdBy")]
        public long CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long ModifiedBy { get; set; }

        [JsonProperty("ownerId")]
        public long OwnerId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("teamId")]
        public long TeamId { get; set; }

        [JsonProperty("allAccessibleTeamIds")]
        public List<long> AllAccessibleTeamIds { get; set; }

        [JsonProperty("bodyPreview")]
        public string BodyPreview { get; set; }

        [JsonProperty("queueMembershipIds")]
        public List<object> QueueMembershipIds { get; set; }

        [JsonProperty("bodyPreviewIsTruncated")]
        public bool BodyPreviewIsTruncated { get; set; }

        [JsonProperty("bodyPreviewHtml")]
        public string BodyPreviewHtml { get; set; }

        [JsonProperty("gdprDeleted")]
        public bool GdprDeleted { get; set; }
    }
}
