using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NightlyCode.Core.Conversion;
using Vertical.HubSpot.Api.Data;
using Vertical.HubSpot.Api.Extensions;
using Vertical.HubSpot.Api.Models;

namespace Vertical.HubSpot.Api.Deals {
    /// <summary>
    /// api used to manage deals in hubspot
    /// </summary>
    public class DealsApi : IDealsApi {
        readonly HubSpotRestClient rest;
        readonly ModelRegistry registry;

        /// <summary>
        /// creates a new <see cref="DealsApi"/>
        /// </summary>
        /// <param name="rest">rest connection to hubspot</param>
        /// <param name="registry">access to entity models</param>
        internal DealsApi(HubSpotRestClient rest, ModelRegistry registry) {
            this.rest = rest;
            this.registry = registry;
        }

        T ToDeal<T>(JObject deal, EntityModel model)
            where T : HubSpotDeal {
            T result = Activator.CreateInstance<T>();
            result.ID = deal.Value<long>("dealId");
            result.IsDeleted = deal.Value<bool>("isDeleted");

            JObject associations = (JObject)deal["associations"];
            result.Contacts = associations.ContainsKey("associatedVids") ? associations["associatedVids"].Values<long>().ToArray() : new long[0];
            result.Companies = associations.ContainsKey("associatedCompanyIds") ? associations["associatedCompanyIds"].Values<long>().ToArray() : new long[0];
            result.Deals = associations.ContainsKey("associatedDealIds") ? associations["associatedDealIds"].Values<long>().ToArray() : new long[0];

            JObject responseproperties = (JObject)deal["properties"];
            foreach(KeyValuePair<string, PropertyInfo> property in model.Properties) {
                if(responseproperties.ContainsKey(property.Key))
                    property.Value.SetValue(result, Converter.Convert(responseproperties[property.Key].Value<object>("value"), property.Value.PropertyType));
            }

            return result;
        }

        /// <summary>
        /// creates a new deal in hubspot
        /// </summary>
        /// <typeparam name="T">type of deal to create</typeparam>
        /// <param name="dealdata">data of deal to create</param>
        /// <returns>created deal</returns>
        public async Task<T> Create<T>(T dealdata)
        where T : HubSpotDeal {
            EntityModel model = registry.Get(typeof(T));

            JObject request = new JObject {
                ["associations"] = new JObject {
                    ["associatedCompanyIds"] = new JArray(dealdata.Companies ?? new long[0]),
                    ["associatedVids"] = new JArray(dealdata.Contacts ?? new long[0])
                }
            };

            JArray properties = new JArray();
            foreach(KeyValuePair<string, PropertyInfo> property in model.Properties) {
                properties.Add(new JObject {
                    ["name"] = property.Key,
                    ["value"] = Convert.ToString(property.Value.GetValue(dealdata), CultureInfo.InvariantCulture)
                });
            }

            request["properties"] = properties;

            JObject response = await rest.Post<JObject>("deals/v1/deal", request);
            return ToDeal<T>(response, model);
        }

        /// <summary>
        /// updates properties of a deal
        /// </summary>
        /// <typeparam name="T">type of deal model to update</typeparam>
        /// <param name="dealdata">data to update</param>
        /// <returns>updated deal</returns>
        public async Task<T> Update<T>(T dealdata)
            where T : HubSpotDeal {
            EntityModel model = registry.Get(typeof(T));

            JObject request = new JObject();
            JArray properties = new JArray();
            foreach(KeyValuePair<string, PropertyInfo> property in model.Properties) {
                properties.Add(new JObject {
                    ["name"] = property.Key,
                    ["value"] = Convert.ToString(property.Value.GetValue(dealdata), CultureInfo.InvariantCulture)
                });
            }

            request["properties"] = properties;

            JObject response = await rest.Put<JObject>($"deals/v1/deal/{dealdata.ID}", request);
            return ToDeal<T>(response, model);
        }

        /// <summary>
        /// updates multiple deals in a batch request
        /// </summary>
        /// <typeparam name="T">type of deal model</typeparam>
        /// <param name="deals">deals to update</param>
        public async Task Update<T>(T[] deals)
            where T : HubSpotDeal {
            EntityModel model = registry.Get(typeof(T));

            JArray request = new JArray();
            foreach(T deal in deals) {
                JObject requestobject = new JObject {
                    ["objectId"] = deal.ID
                };

                JArray properties = new JArray();
                foreach(KeyValuePair<string, PropertyInfo> property in model.Properties) {
                    properties.Add(new JObject {
                        ["name"] = property.Key,
                        ["value"] = Convert.ToString(property.Value.GetValue(deal), CultureInfo.InvariantCulture)
                    });
                }

                requestobject["properties"] = properties;
                request.Add(requestobject);
            }

            await rest.Post<JObject>("deals/v1/batch-async/update", request);
        }

        IEnumerable<Parameter> GetListParameters(long? offset, params string[] properties) {
            yield return new Parameter("includeAssociations", "true");
            yield return new Parameter("limit", "250");
            if(offset.HasValue)
                yield return new Parameter("offset", offset?.ToString());
            foreach(string property in properties)
                yield return new Parameter("properties", property);
        }

        IEnumerable<Parameter> GetRecentParameters(DateTime? since, long? offset) {
            yield return new Parameter("count", "100");
            if(offset.HasValue)
                yield return new Parameter("offset", offset?.ToString());
            if(since.HasValue)
                yield return new Parameter("since", since.Value.ToUnixTimestamp().ToString());
        }

        /// <summary>
        /// lists all deals and returns a page of the result
        /// </summary>
        /// <typeparam name="T">type of deal model</typeparam>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <param name="properties">properties to include in result</param>
        /// <returns>one page of deal list response</returns>
        public async Task<PageResponse<T>> ListPage<T>(long? offset = null, params string[] properties)
            where T : HubSpotDeal {
            EntityModel model = registry.Get(typeof(T));

            JObject response = await rest.Get<JObject>("deals/v1/deal/paged", GetListParameters(offset, properties).ToArray());

            var hasMore = response.Value<bool>("has-more");
            return new PageResponse<T> {
                HasMore = hasMore,
                Offset = hasMore ? response.Value<long?>("offset") : null,
                Data = response.GetValue("deals").OfType<JObject>().Select(d => ToDeal<T>(d, model)).ToArray()
            };
        }

        /// <summary>
        /// lists all deals
        /// </summary>
        /// <typeparam name="T">type of deal to return</typeparam>
        /// <param name="properties">properties to include in result</param>
        /// <returns>list of all deals</returns>
        public async Task<T[]> List<T>(params string[] properties)
            where T : HubSpotDeal {
            List<T> result = new List<T>();
            PageResponse<T> response = null;
            do {
                response = await ListPage<T>(response?.Offset, properties);
                result.AddRange(response.Data);
            } while(response.Offset.HasValue);

            return result.ToArray();
        }

        /// <summary>
        /// get recently modified deals
        /// </summary>
        /// <typeparam name="T">type of deal model</typeparam>
        /// <param name="since">only returns deals modified since this date</param>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <returns>a page of recently modified deals</returns>
        public async Task<PageResponse<T>> RecentlyModifiedPage<T>(DateTime? since, long? offset = null)
            where T : HubSpotDeal {
            EntityModel model = registry.Get(typeof(T));

            JObject response = await rest.Get<JObject>("deals/v1/deal/recent/modified", GetRecentParameters(since, offset).ToArray());

            var hasMore = response.Value<bool>("has-more");
            return new PageResponse<T> {
                HasMore = hasMore,
                Offset = hasMore ? response.Value<long?>("offset") : null,
                Data = response.GetValue("results").OfType<JObject>().Select(d => ToDeal<T>(d, model)).ToArray()
            };
        }

        /// <summary>
        /// get recently created deals
        /// </summary>
        /// <typeparam name="T">type of deal model</typeparam>
        /// <param name="since">only returns deals modified since this date</param>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <returns>a page of recently modified deals</returns>
        public async Task<PageResponse<T>> RecentlyCreatedPage<T>(DateTime? since, long? offset = null)
            where T : HubSpotDeal {
            EntityModel model = registry.Get(typeof(T));

            JObject response = await rest.Get<JObject>("deals/v1/deal/recent/created", GetRecentParameters(since, offset).ToArray());

            return new PageResponse<T> {
                Offset = response.Value<bool>("hasMore") ? response.Value<long?>("offset") : null,
                Data = response.GetValue("results").OfType<JObject>().Select(d => ToDeal<T>(d, model)).ToArray()
            };
        }

        /// <summary>
        /// deletes a deal from hubspot
        /// </summary>
        /// <typeparam name="T">type of deal to delete</typeparam>
        /// <param name="id">deal id</param>
        /// <returns>deleted deal</returns>
        public async Task<T> Delete<T>(long id)
            where T : HubSpotDeal {
            JObject response = await rest.Delete<JObject>($"deals/v1/deal/{id}");
            return ToDeal<T>(response, registry.Get(typeof(T)));
        }

        /// <summary>
        /// get a deal by it's id
        /// </summary>
        /// <typeparam name="T">type of deal model</typeparam>
        /// <param name="id">id of deal to return</param>
        /// <returns>deal data</returns>
        public async Task<T> Get<T>(long id)
            where T : HubSpotDeal {
            JObject response = await rest.Get<JObject>($"deals/v1/deal/{id}");
            return ToDeal<T>(response, registry.Get(typeof(T)));
        }

    }
}
