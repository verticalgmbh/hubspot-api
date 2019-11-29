using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.Associations
{
    /// <summary>
    /// api used to associate objects in crm
    /// </summary>
    public class AssociationApi : IAssociationApi {
        readonly HubSpotRestClient rest;

        /// <summary>
        /// creates a new <see cref="AssociationApi"/>
        /// </summary>
        /// <param name="rest">access to rest api</param>
        internal AssociationApi(HubSpotRestClient rest) {
            this.rest = rest;
        }

        IEnumerable<Parameter> GetListParameters(long? offset)
        {
            yield return new Parameter("limit", "100");
            if (offset.HasValue)
                yield return new Parameter("offset", offset.ToString());
        }

        /// <summary>
        /// Associate 2 CRM objects. You would use this endpoint to associate a ticket with a contact, or to associate a line item object to a deal
        /// </summary>
        /// <param name="fromid">The ID of the object being associated</param>
        /// <param name="toid">The ID of the object the from object is being associated with</param>
        /// <param name="type">The ID of the association definition</param>
        public async Task Create(long fromid, long toid, AssociationType type) {
            JObject request = new JObject {
                ["fromObjectId"] = fromid,
                ["toObjectId"] = toid,
                ["category"] = "HUBSPOT_DEFINED",
                ["definitionId"] = (int) type
            };

            await rest.Put<JObject>("crm-associations/v1/associations", request);
        }

        /// <summary>
        /// creates multiple associations at once
        /// </summary>
        /// <param name="associations">associations to create</param>
        public async Task Create(params Association[] associations) {
            if (associations == null || associations.Length == 0)
                throw new ArgumentException("At least one association needs to be specified. (at least 2 would make sense for this call)");

            JArray request=new JArray();

            foreach (Association association in associations) {
                request.Add(new JObject {
                    ["fromObjectId"] = association.FromID,
                    ["toObjectId"] = association.ToID,
                    ["category"] = "HUBSPOT_DEFINED",
                    ["definitionId"] = (int)association.Type
                });
            }

            await rest.Put<JObject>("crm-associations/v1/associations/create-batch", request);
        }

        /// <summary>
        /// Get the IDs of objects associated with the given object, based on the specified association type
        /// </summary>
        /// <param name="objectid">id of which to list associations</param>
        /// <param name="type">type of assocation to list</param>
        /// <param name="offset">page token used to get next page in multipage result (optional)</param>
        /// <returns>one page of list results</returns>
        public async Task<PageResponse<long>> ListPage(long objectid, AssociationType type, long? offset=null) {
            JObject response = await rest.Get<JObject>($"crm-associations/v1/associations/{objectid}/HUBSPOT_DEFINED/{(int) type}", GetListParameters(offset).ToArray());

            return new PageResponse<long> {
                Offset = response.Value<bool>("hasMore") ? response.Value<long?>("offset") : null,
                Data = response.ContainsKey("results") ? response["results"].Values<long>().ToArray() : new long[0]
            };
        }

        /// <summary>
        /// Get the IDs of objects associated with the given object, based on the specified association type
        /// </summary>
        /// <param name="objectid">id of which to list associations</param>
        /// <param name="type">type of assocation to list</param>
        /// <returns>list results</returns>
        /// <returns>all results of the specified type</returns>
        public async Task<long[]> List(long objectid, AssociationType type)
        {
            List<long> result = new List<long>();
            PageResponse<long> response = null;
            do
            {
                response = await ListPage(objectid, type, response?.Offset);
                result.AddRange(response.Data);
            } while (response.Offset.HasValue);

            return result.ToArray();
        }

        /// <summary>
        /// Delete an association between 2 CRM objects
        /// </summary>
        /// <param name="fromid">The ID of the object you want to remove the association from</param>
        /// <param name="toid">The ID of the currently associated object that you're removing the association from</param>
        /// <param name="type">The ID of the association definition</param>
        public async Task Delete(long fromid, long toid, AssociationType type)
        {
            JObject request = new JObject
            {
                ["fromObjectId"] = fromid,
                ["toObjectId"] = toid,
                ["category"] = "HUBSPOT_DEFINED",
                ["definitionId"] = (int)type
            };

            await rest.Put<JObject>("crm-associations/v1/associations/delete", request);
        }

        /// <summary>
        /// The JSON data in the PUT request will be a list of items each representing a single association that you want to delete
        /// </summary>
        /// <param name="associations">associations to delete</param>
        public async Task Delete(params Association[] associations)
        {
            if (associations == null || associations.Length == 0)
                throw new ArgumentException("At least one association needs to be specified. (at least 2 would make sense for this call)");

            JArray request = new JArray();

            foreach (Association association in associations)
            {
                request.Add(new JObject
                {
                    ["fromObjectId"] = association.FromID,
                    ["toObjectId"] = association.ToID,
                    ["category"] = "HUBSPOT_DEFINED",
                    ["definitionId"] = (int)association.Type
                });
            }

            await rest.Put<JObject>("crm-associations/v1/associations/delete-batch", request);
        }

    }
}
