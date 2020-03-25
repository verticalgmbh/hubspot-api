using Newtonsoft.Json;

namespace Vertical.HubSpot.Api.BlogPost
{
    public class HubSpotBlogPostList<T> where T : HubSpotBlogPost
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("objects")]
        public T[] BlogPosts { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }
    }
}