using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Vertical.HubSpot.Api.Data;
using Vertical.HubSpot.Api.Extensions;
using Vertical.HubSpot.Api.Models;

namespace Vertical.HubSpot.Api.Engagements
{
    /// <summary>
    /// access to contacts api
    /// </summary>
    public class EngagementsApi : IEngagementsApi
    {
        private readonly HubSpotOptions _options;
        readonly ModelRegistry models;
        readonly HubSpotRestClient rest;

        /// <summary>
        /// creates a new <see cref="EngagementApi"/>
        /// </summary>
        /// <param name="rest">rest client to call api</param>
        /// <param name="models">model registry used to access entity models</param>
        internal EngagementsApi(HubSpotOptions options, HubSpotRestClient rest, ModelRegistry models) {
            _options = options;
            this.rest = rest;
            this.models = models;
        }

        /// <summary>
        /// get a contact by id
        /// </summary>
        /// <typeparam name="T">type of contact model</typeparam>
        /// <param name="id">id of contact to return</param>
        /// <returns>contact data</returns>
        public async Task<T> GetEngagement<T>(long id)
            where T : HubSpotEngagementResult
        {
            JObject response = await rest.Get<JObject>($"/engagements/v1/engagements/{id}");
            return ToEngagementResult<T>(response);
        }

        public Task<JObject> DeleteEngagement(long id)
        {
            return rest.Delete<JObject>($"/engagements/v1/engagements/{id}");
        }

        public async Task<T> CreateEngagement<T>(HubSpotEngagementResult data)
            where T : HubSpotEngagementResult
        {
            var requestData = JToken.FromObject(data);
            JObject response = await rest.Post<JObject>($"/engagements/v1/engagements", requestData);
            return ToEngagementResult<T>(response);
        }

        public async Task<T> UpdateEngagement<T>(long id, HubSpotEngagementResult data)
            where T : HubSpotEngagementResult
        {
            var requestData = JToken.FromObject(data);
            
            JObject response = await rest.Patch<JObject>($"/engagements/v1/engagements/{id}", requestData);
            return ToEngagementResult<T>(response);
        }


        IEnumerable<Parameter> GetListParameters(long? offset, int? limit)
        {
            yield return new Parameter("limit", (limit ?? 20).ToString());
            if (offset.HasValue)
                yield return new Parameter("offset", offset.ToString());
        }

        public async Task<PageResponse<T>> ListAssociatedEngagements<T>(ObjectType type, long contactId, long? offset = null, int? count = null)
            where T : HubSpotEngagementResult
        {
            JObject response = await rest.Get<JObject>($"engagements/v1/engagements/associated/{type}/{contactId}/paged", GetListParameters(offset, count ?? 100).ToArray());

            var hasMore = response.Value<bool>("has-more");
            return new PageResponse<T>
            {
                HasMore = hasMore,
                Offset = hasMore ? response.Value<long?>("offset") : null,
                Data = response.GetValue("results").OfType<JObject>().Select(d => ToEngagementResult<T>(d)).ToArray()
            };
        }

        private static T ToEngagementResult<T>(JObject engagement)
            where T : HubSpotEngagementResult
        {
            return engagement.ToObject<T>();
        }
    }

    
}