using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.BlogAuthor
{
    public class HubSpotBlogAuthor
	{
        [Name("avatar")]
        public string Avatar { get; set; }

        [Name("bio")]
        public string Bio { get; set; }
        
        [Name("created")]
        public long Created { get; set; }
        
        [Name("deleted_at")]
        public long DeletedAt { get; set; }

        [Name("display_name")]
        public string DisplayName { get; set; }

        [Name("email")]
        public string Email { get; set; }

        [Name("facebook")]
        public string Facebook { get; set; }

        [Name("full_name")]
        public string FullName { get; set; }

        [Name("has_social_profiles")]
        public bool HasSocialProfiles { get; set; }

        [Name("id")]
        public long Id { get; set; }

        [Name("linkedin")]
        public string Linkedin { get; set; }

        [Name("portal_id")]
        public long PortalId { get; set; }

        [Name("slug")]
        public string Slug { get; set; }

        [Name("twitter")]
        public string Twitter { get; set; }

        [Name("twitter_username")]
        public string TwitterUsername { get; set; }

        [Name("updated")]
        public long Updated { get; set; }

        [Name("website")]
        public string Website { get; set; }
    }
}