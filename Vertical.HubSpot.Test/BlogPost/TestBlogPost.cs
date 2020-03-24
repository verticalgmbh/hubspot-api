using System.Threading.Tasks;
using Vertical.HubSpot.Api.BlogPost;
using Vertical.HubSpot.Test.Test;
using Xunit;
using Xunit.Abstractions;

namespace Vertical.HubSpot.Test.BlogPost
{
    public class TestBlogPost : TestBase
    {
        private readonly ITestOutputHelper _outputHelper;

        public TestBlogPost(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task ListBlogPosts()
        {
            var hubspot = GetHubSpot();

            var list = await hubspot.BlogPosts.ListPage<HubSpotBlogPost>(null, 2);
            Assert.True(list?.BlogPosts?.Length>0, "No blogposts found");

            foreach (var blogPost in list.BlogPosts)
            {
                _outputHelper.WriteLine($"list:blogPost.id={blogPost.Id}");
            }

        }

        [Fact]
        public async Task GetBlogPost()
        {
            var hubspot = GetHubSpot();

            var blogPost = await hubspot.BlogPosts.Get<HubSpotBlogPost>(26716152419);
            _outputHelper.WriteLine($"get:blogPost.id={blogPost.Id}");
        }
    }
}