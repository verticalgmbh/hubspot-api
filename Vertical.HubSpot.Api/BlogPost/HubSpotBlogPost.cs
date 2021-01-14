using System.Collections.Generic;
using Newtonsoft.Json;
using Vertical.HubSpot.Api.BlogAuthor;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.BlogPost
{
    public class HubSpotBlogPost
    {
        /// <summary>
        /// If True, the post will not show up in your dashboard, although the post will still be live
        /// </summary>
        [JsonProperty("id")]
        [HubspotId]
        public long Id { get; set; }

        /// <summary>
        /// If True, the post will not show up in your dashboard, although the post will still be live
        /// </summary>
        [JsonProperty("archived")]
        public bool Archived { get; set; }

        /// <summary>
        /// The integer id of the blog author, look up via the blog authors end point /content/api/v2/blog-authors
        /// </summary>
        [JsonProperty("blog_author_id")]
        public long BlogAuthorId { get; set; }

        /// <summary>
        /// The guid of the marketing campaign this post is associated with
        /// </summary>
        [JsonProperty("campaign")]
        public string Campaign { get; set; }

        /// <summary>
        /// The name of the marketing campaign this post is associated with
        /// </summary>
        [JsonProperty("campaign_name")]
        public string CampaignName { get; set; }

        /// <summary>
        /// The id of the blog that this post belongs to.Get the id by looking at the blog API endpoint /content/api/v2/blogs
        /// </summary>
        [JsonProperty("content_group_id")]
        public long ContentGroupId { get; set; }

        /// <summary>
        /// When the post was first created, in milliseconds since the epoch
        /// </summary>
        [JsonProperty("created")]
        public long Created { get; set; }

        /// <summary>
        /// When the post was deleted, in milliseconds since the epoch. Zero if the blog post was never deleted. Use a DELETE request to delete the post, do not set this directly.
        /// </summary>
        [JsonProperty("deleted_at")]
        public long DeletedAt { get; set; }

        /// <summary>
        /// Custom HTML for embed codes, javascript that should be placed before the</body> tag of the page
        /// </summary>
        [JsonProperty("footer_html")]
        public string FooterHtml { get; set; }

        /// <summary>
        /// Custom HTML for embed codes, javascript, etc.that goes in the<head> tag of the page
        /// </summary>
        [JsonProperty("head_html")]
        public string HeadHtml { get; set; }

        /// <summary>
        /// True if the post is still a draft, invisible to the public. Gets changed when the /publish-action API endpoint is called
        /// </summary>
        [JsonProperty("is_draft")]
        public string IsDraft { get; set; }

        /// <summary>
        /// list A JSON list of keywords and their GUIDs.This list contains key value pairs of "keyword" and "keyword_guid". For example:
        ///        "keyword" : "sandwiches",
        ///        "keyword_guid" : "ee7c2292-7a2f-4f0f-81fb-ad0bd9ca4fcb"
        /// The GUID is available from the Keywords API.This list adds keywords to the Optimization tab in HubSpot.
        /// </summary>
        [JsonProperty("keywords")]
        public KeyValuePair<string,string>[] Keywords { get; set; }

        /// <summary>
        /// A description that goes in &lt;meta&gt; tag on the page
        /// </summary>
        [JsonProperty("meta_description")]
        public string MetaDescription { get; set; }

        /// <summary>
        /// The internal name of the blog post
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The HTML of the main post body
        /// </summary>
        [JsonProperty("post_body")]
        public string PostBody { get; set; }

        /// <summary>
        /// The summary of the blog post that will appear on the main listing page
        /// </summary>
        [JsonProperty("post_summary")]
        public string PostSummary { get; set; }

        /// <summary>
        /// The date the blog post is to be published at in milliseconds since the unix epoch.
        /// </summary>
        [JsonProperty("publish_date")] 
        public long PublishDate { get; set; }

        /// <summary>
        /// Set this to true if you want to be published immediately when the schedule publish endpoint is called, and to ignore the publish_date setting
        /// </summary>
        [JsonProperty("publish_immediately")]
        public string PublishImmediately { get; set; }

        /// <summary>
        /// The path of the URL on which the post will live.Changing this will change the URL.
        /// </summary>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        /// A json list of topic ids from the topics API ( /content/api/v2/topics )
        /// </summary>
        [JsonProperty("topic_ids")]
        public List<long> TopicIds { get; set; }

        /// <summary>
        /// When the post was last updated, in milliseconds since the epoch
        /// </summary>
        [JsonProperty("updated")]
        public long Updated { get; set; }

        /// <summary>
        /// The full URL with domain and scheme to the blog post.Will return a 404 if the post is not yet published.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("blog_author")]
        public HubSpotBlogAuthor BlogAuthor { get; set; }

        [JsonProperty("featured_image")]
        public string FeaturedImage { get; set; }

        [JsonProperty("html_title")]
        public string HtmlTitle { get; set; }

        [JsonProperty("published_url")]
        public string PublishedUrl { get; set; }

    }
}