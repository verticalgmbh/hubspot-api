using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Vertical.HubSpot.Api.Data;
using Vertical.HubSpot.Api.Extensions;
using Vertical.HubSpot.Api.Models;

namespace Vertical.HubSpot.Api.Owners
{

    /// <summary>
    /// api for owners in hubspot
    /// </summary>
    public class OwnersApi : IOwnersApi
    {
        readonly HubSpotRestClient rest;
        readonly ModelRegistry models;

        /// <summary>
        /// creates a new <see cref="OwnersApi"/>
        /// </summary>
        /// <param name="rest">rest access to hubspot</param>
        public OwnersApi(HubSpotRestClient rest, ModelRegistry models)
        {
            this.rest = rest;
            this.models = models;
        }

        IEnumerable<Parameter> GetListParameters(string email = null, bool includeinactive = false)
        {
            if (!string.IsNullOrEmpty(email))
                yield return new Parameter("email", email);
            if (includeinactive)
                yield return new Parameter("includeInactive", "true");
        }

        /// <summary>
        /// Returns all of the owners that exist inside of HubSpot
        /// </summary>
        /// <param name="email">filters list for owners with specific e-mails (optional)</param>
        /// <param name="includeinactive">includes inactive owners in result</param>
        /// <returns></returns>
        public async Task<HubSpotOwner[]> List(string email = null, bool includeinactive = false)
        {
            JArray response = await rest.Get<JArray>("owners/v2/owners/", GetListParameters(email, includeinactive).ToArray());
            return response.ToObject<HubSpotOwner[]>();
        }

        public async Task<HubSpotOwner> Get<T>(long id)
            where T : HubSpotOwner
        {
            JObject response = await rest.Get<JObject>($"owners/v2/owners/{id}");
            return response.ToOwner<T>();
        }
    }
}
