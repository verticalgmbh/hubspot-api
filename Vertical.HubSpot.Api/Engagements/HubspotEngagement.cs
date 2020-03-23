namespace Vertical.HubSpot.Api.Engagements
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class HubSpotEngagements
    {
        [JsonProperty("results")]
        public List<HubSpotEngagementResult> Results { get; set; }

        [JsonProperty("hasMore")]
        public bool HasMore { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }
    }

    public class HubSpotEngagementResult
    {
        [JsonProperty("engagement")]
        public HubspotEngagement Engagement { get; set; }

        [JsonProperty("associations")]
        public HubSpotEngagementAssociations Associations { get; set; }

        [JsonProperty("attachments")]
        public List<object> Attachments { get; set; }

        [JsonProperty("scheduledTasks")]
        public List<HubspotScheduledTask> ScheduledTasks { get; set; }

        [JsonProperty("metadata")]
        public HubspotEngagementMetadata Metadata { get; set; }
    }

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

    public class HubspotEngagementMetadata
    {
        [JsonProperty("startTime")]
        public long StartTime { get; set; }

        [JsonProperty("endTime")]
        public long EndTime { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("externalUrl")]
        public Uri ExternalUrl { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("sourceId")]
        public string SourceId { get; set; }

        [JsonProperty("createdFromLinkId")]
        public long CreatedFromLinkId { get; set; }

        [JsonProperty("preMeetingProspectReminders")]
        public List<object> PreMeetingProspectReminders { get; set; }
    }

    public class HubspotScheduledTask
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
