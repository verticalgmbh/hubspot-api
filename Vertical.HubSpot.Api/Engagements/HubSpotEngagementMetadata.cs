using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vertical.HubSpot.Api.Engagements
{
    public class HubspotEngagementMetadata
    {
        [JsonProperty("startTime")]
        public long? StartTime { get; set; }

        [JsonProperty("endTime")]
        public long? EndTime { get; set; }

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
        public long? CreatedFromLinkId { get; set; }

        [JsonProperty("preMeetingProspectReminders")]
        public List<object> PreMeetingProspectReminders { get; set; }

        [JsonProperty("cc")]
        public List<HubspotEngangementContact> Cc { get; set; }

        [JsonProperty("bcc")]
        public List<HubspotEngangementContact> Bcc { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("from")]
        public HubspotEngangementContact From { get; set; }

        [JsonProperty("to")]
        public List<HubspotEngangementContact> To { get; set; }
        [JsonProperty("sender")]
        public HubspotEngangementContact Sender { get; set; }
    }
}