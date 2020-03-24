using System.Threading.Tasks;
using Vertical.HubSpot.Api.BlogPost;
using Vertical.HubSpot.UnitTest.Test;
using Xunit;

namespace Vertical.HubSpot.UnitTest.BlogPost
{
    public class TestBlogPost : TestBase
    {
        [Fact]
        public async Task ListBlogPosts()
        {
            var hubspotApi = GetHubSpotApi();

            var list = await hubspotApi.BlogPosts.ListPage<HubSpotBlogPost>(null, 2);
        }

        [Fact]
        public async Task GetBlogPost()
        {
            var hubspotApi = GetHubSpotApi();

            var list = await hubspotApi.BlogPosts.ListPage<HubSpotBlogPost>(null, 2);
        }
    }
}