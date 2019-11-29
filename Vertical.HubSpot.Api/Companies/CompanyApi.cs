using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NightlyCode.Core.Conversion;
using Vertical.HubSpot.Api.Data;
using Vertical.HubSpot.Api.Models;

namespace Vertical.HubSpot.Api.Companies {
    /// <summary>
    /// access to company api
    /// </summary>
    public class CompanyApi : ICompanyApi {
        readonly HubSpotRestClient rest;
        readonly ModelRegistry registry;

        /// <summary>
        /// creates a new <see cref="CompanyApi"/>
        /// </summary>
        /// <param name="rest">rest client</param>
        /// <param name="registry">access to entity models</param>
        internal CompanyApi(HubSpotRestClient rest, ModelRegistry registry) {
            this.rest = rest;
            this.registry = registry;
        }

        T ToCompany<T>(JObject company, EntityModel model)
        where T : HubSpotCompany
        {
            T result = Activator.CreateInstance<T>();
            result.ID = company.Value<long>("companyId");
            result.IsDeleted = company.Value<bool>("isDeleted");

            JObject responseproperties = (JObject)company["properties"];
            foreach (KeyValuePair<string, PropertyInfo> property in model.Properties)
            {
                if (responseproperties.ContainsKey(property.Key))
                    property.Value.SetValue(result, Converter.Convert(responseproperties[property.Key].Value<object>("value"), property.Value.PropertyType));
            }

            return result;
        }

        /// <summary>
        /// creates a new company in hubspot
        /// </summary>
        /// <typeparam name="T">type of company</typeparam>
        /// <param name="company">company data to create</param>
        /// <returns>created entity</returns>
        public async Task<T> Create<T>(T company)
        where T : HubSpotCompany
        {
            EntityModel model = registry.Get(typeof(T));

            JObject request = new JObject();
            JArray properties = new JArray();
            foreach (KeyValuePair<string, PropertyInfo> property in model.Properties) {
                properties.Add(new JObject {
                    ["name"] = property.Key,
                    ["value"] = property.Value.GetValue(company)?.ToString()
                });
            }

            request["properties"] = properties;

            JObject response = await rest.Post<JObject>("companies/v2/companies", request);
            return ToCompany<T>(response, model);
        }

        /// <summary>
        /// updates a company in hubspot
        /// </summary>
        /// <typeparam name="T">type of company</typeparam>
        /// <param name="company">company data to update</param>
        /// <returns>updated company</returns>
        public async Task<T> Update<T>(T company)
        where T : HubSpotCompany
        {
            EntityModel model = registry.Get(typeof(T));

            JObject request = new JObject();
            JArray properties = new JArray();
            foreach (KeyValuePair<string, PropertyInfo> property in model.Properties)
            {
                properties.Add(new JObject
                {
                    ["name"] = property.Key,
                    ["value"] = property.Value.GetValue(company)?.ToString()
                });
            }

            request["properties"] = properties;

            JObject response = await rest.Put<JObject>($"companies/v2/companies/{company.ID}", request);
            return ToCompany<T>(response, model);
        }

        /// <summary>
        /// updates several companies in hubspot
        /// </summary>
        /// <typeparam name="T">type of company model</typeparam>
        /// <param name="companies">data for companies to update</param>
        public async Task BatchUpdate<T>(params T[] companies)
            where T : HubSpotCompany {
            EntityModel model = registry.Get(typeof(T));

            JArray request = new JArray();
            foreach (T company in companies) {
                JObject updaterequest = new JObject {
                    ["objectId"] = company.ID
                };

                JArray properties = new JArray();
                foreach (KeyValuePair<string, PropertyInfo> property in model.Properties)
                {
                    properties.Add(new JObject
                    {
                        ["name"] = property.Key,
                        ["value"] = property.Value.GetValue(company)?.ToString()
                    });
                }
                updaterequest["properties"] = properties;
                request.Add(updaterequest);
            }

            await rest.Post<JObject>("companies/v1/batch-async/update", request);
        }

        IEnumerable<Parameter> GetListParameters(long? offset, params string[] properties) {
            yield return new Parameter("limit", "250");
            if (offset.HasValue)
                yield return new Parameter("offset", offset?.ToString());
            foreach (string property in properties)
                yield return new Parameter("properties", property);
        }

        /// <summary>
        /// lists all companies and returns a page of the result
        /// </summary>
        /// <typeparam name="T">type of company model</typeparam>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <param name="properties">properties to include in result</param>
        /// <returns>one page of company list response</returns>
        public async Task<PageResponse<T>> ListPage<T>(long? offset=null, params string[] properties)
            where T:HubSpotCompany
        {
            EntityModel model = registry.Get(typeof(T));

            JObject response = await rest.Get<JObject>("companies/v2/companies/paged", GetListParameters(offset, properties).ToArray());

            return new PageResponse<T> {
                Offset = response.Value<bool>("has-more") ? response.Value<long?>("offset") : null,
                Data = response.GetValue("companies").OfType<JObject>().Select(d => ToCompany<T>(d, model)).ToArray()
            };
        }

        /// <summary>
        /// get recently modified companies
        /// </summary>
        /// <typeparam name="T">type of company model</typeparam>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <returns>a page of recently modified companies</returns>
        public async Task<PageResponse<T>> RecentlyModifiedPage<T>(long? offset = null)
            where T : HubSpotCompany
        {
            EntityModel model = registry.Get(typeof(T));

            JObject response = await rest.Get<JObject>("companies/v2/companies/recent/modified", GetListParameters(offset).ToArray());

            return new PageResponse<T>
            {
                Offset = response.Value<bool>("hasMore") ? response.Value<long?>("offset") : null,
                Data = response.GetValue("results").OfType<JObject>().Select(d => ToCompany<T>(d, model)).ToArray()
            };
        }

        /// <summary>
        /// get recently created companies
        /// </summary>
        /// <typeparam name="T">type of company model</typeparam>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <returns>a page of recently created companies</returns>
        public async Task<PageResponse<T>> RecentlyCreatedPage<T>(long? offset = null)
            where T : HubSpotCompany
        {
            EntityModel model = registry.Get(typeof(T));

            JObject response = await rest.Get<JObject>("companies/v2/companies/recent/created", GetListParameters(offset).ToArray());

            return new PageResponse<T>
            {
                Offset = response.Value<bool>("hasMore") ? response.Value<long?>("offset") : null,
                Data = response.GetValue("results").OfType<JObject>().Select(d => ToCompany<T>(d, model)).ToArray()
            };
        }

        /// <summary>
        /// searches for companies by domain criteria
        /// </summary>
        /// <typeparam name="T">type of company model</typeparam>
        /// <param name="domain">domain to search for</param>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <param name="properties">properties to include in search result</param>
        /// <returns>a page of recently created companies</returns>
        public async Task<PageResponse<T>> SearchByDomainPage<T>(string domain, long? offset = null, params string[] properties)
            where T : HubSpotCompany
        {
            EntityModel model = registry.Get(typeof(T));

            JObject request = new JObject {
                ["requestOptions"] = new JObject {
                    ["properties"] = new JArray(properties.Cast<object>().ToArray())
                },
                ["offset"] = new JObject {
                    ["isPrimary"] = true,
                    ["companyId"] = offset ?? 0
                }
            };
            JObject response = await rest.Post<JObject>($"companies/v2/domains/{domain}/companies", request);

            return new PageResponse<T>
            {
                Offset = response.Value<bool>("hasMore") ? response.Value<long?>("offset") : null,
                Data = response.GetValue("results").OfType<JObject>().Select(d => ToCompany<T>(d, model)).ToArray()
            };
        }

        /// <summary>
        /// lists all companies
        /// </summary>
        /// <typeparam name="T">type of company to return</typeparam>
        /// <param name="properties">properties to include in result</param>
        /// <returns>list of all companies</returns>
        public async Task<T[]> List<T>(params string[] properties)
            where T : HubSpotCompany {
            List<T> result=new List<T>();
            PageResponse<T> response = null;
            do {
                response = await ListPage<T>(response?.Offset, properties);
                result.AddRange(response.Data);
            } while (response.Offset.HasValue);

            return result.ToArray();
        }

        /// <summary>
        /// deletes a company from hubspot
        /// </summary>
        /// <typeparam name="T">type of company to delete</typeparam>
        /// <param name="id">company id</param>
        /// <returns>deleted company</returns>
        public async Task<T> Delete<T>(long id)
            where T : HubSpotCompany {
            JObject response = await rest.Delete<JObject>($"companies/v2/companies/{id}");
            return ToCompany<T>(response, registry.Get(typeof(T)));
        }

        /// <summary>
        /// get a company by it's id
        /// </summary>
        /// <typeparam name="T">type of company model</typeparam>
        /// <param name="id">id of company to return</param>
        /// <returns>company data</returns>
        public async Task<T> Get<T>(long id)
            where T : HubSpotCompany {
            JObject response = await rest.Get<JObject>($"companies/v2/companies/{id}");
            return ToCompany<T>(response, registry.Get(typeof(T)));
        }
    }
}
