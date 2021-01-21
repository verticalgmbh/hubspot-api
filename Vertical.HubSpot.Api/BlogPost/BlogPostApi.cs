using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Vertical.HubSpot.Api.Data;
using Vertical.HubSpot.Api.Models;

namespace Vertical.HubSpot.Api.BlogPost
{
    public class BlogPostApi : IBlogPostApi
    {
        private readonly HubSpotRestClient _rest;
        private readonly ModelRegistry _model;

        internal BlogPostApi(HubSpotRestClient rest, ModelRegistry model)
        {
            _rest = rest;
            _model = model;
        }

        private IEnumerable<Parameter> GetListParameters(
            long? offset,
            long? limit,
            string state,
            string orderby,
            params KeyValuePair<string, string>[] properties)
        {
            yield return new Parameter(nameof(limit), (limit ?? 20L).ToString());
            if (offset.HasValue)
                yield return new Parameter(nameof(offset), offset.ToString());
            if (!string.IsNullOrWhiteSpace(state))
                yield return new Parameter(nameof(state), state);
            if (!string.IsNullOrWhiteSpace(orderby))
                yield return new Parameter("order_by", state);
            if( properties?.Length>0)
                foreach (var property in properties)
                {
                    yield return new Parameter(property.Key,property.Value);
                }
        }

        public async Task<HubSpotBlogPostList<T>> ListPage<T>(long? offset = null, int? limit = null, string state = "PUBLISHED", params KeyValuePair<string, string>[] properties)  where T : HubSpotBlogPost
        {
            JObject response = await _rest.Get<JObject>("content/api/v2/blog-posts", GetListParameters(offset, limit, state, "-publish_date", properties).ToArray());

            return response.ToObject<HubSpotBlogPostList<T>>();
        }

        public async Task<T> Get<T>(long id) where T : HubSpotBlogPost
        {
            return (await _rest.Get<JToken>("/content/api/v2/blog-posts/"+ id)).ToObject<T>();
        }

    }
}