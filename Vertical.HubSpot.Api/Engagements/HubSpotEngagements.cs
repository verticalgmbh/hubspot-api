using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vertical.HubSpot.Api.Engagements
{
    public class HubSpotEngagements
    {
        [JsonProperty("results")]
        public List<HubSpotEngagementResult> Results { get; set; }

        [JsonProperty("hasMore")]
        public bool HasMore { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }
    }
}