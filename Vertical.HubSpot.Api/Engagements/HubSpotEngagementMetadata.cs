using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Vertical.HubSpot.Api.Engagements
{
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
}