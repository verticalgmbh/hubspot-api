using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vertical.HubSpot.Api.BlogPost
{
    public interface IBlogPostApi
    {
        Task<HubSpotBlogPostList<T>> ListPage<T>(long? offset = null, int? limit = null, string state = "PUBLISHED", params KeyValuePair<string, string>[] properties) where T : HubSpotBlogPost;
        Task<T> Get<T>(long id) where T : HubSpotBlogPost;
    }
}