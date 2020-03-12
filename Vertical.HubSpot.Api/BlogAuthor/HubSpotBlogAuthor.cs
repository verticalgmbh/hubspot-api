using Newtonsoft.Json;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.BlogAuthor
{
    public class HubSpotBlogAuthor
	{
        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("gravatar_url")]
        public string GravatarUrl { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }
        
        [JsonProperty("created")]
        public long Created { get; set; }
        
        [JsonProperty("deleted_at")]
        public long DeletedAt { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("facebook")]
        public string Facebook { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("has_social_profiles")]
        public bool HasSocialProfiles { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("linkedin")]
        public string Linkedin { get; set; }

        [JsonProperty("portal_id")]
        public long PortalId { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        [JsonProperty("twitter_username")]
        public string TwitterUsername { get; set; }

        [JsonProperty("updated")]
        public long Updated { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }
    }
}