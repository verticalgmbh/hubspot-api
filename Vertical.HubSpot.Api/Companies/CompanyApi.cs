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
    public class CompanyApi {
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

            JObject response = await rest.Post("companies/v2/companies", request);
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

            JObject response = await rest.Put($"companies/v2/companies/{company.ID}", request);
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

            await rest.Post("companies/v1/batch-async/update", request);
        }

        IEnumerable<Parameter> GetListParameters(long? offset, params string[] properties) {
            yield return new Parameter("limit", "250");
            if (offset.HasValue)
                yield return new Parameter("offset", offset?.ToString());
            foreach (string property in properties)
                yield return new Parameter("properties", property);
        }

        /// <summary>
        /// lists all companies
        /// </summary>
        /// <typeparam name="T">type of company to return</typeparam>
        /// <param name="properties">properties to include in result</param>
        /// <returns>list of all companies</returns>
        public async Task<T[]> List<T>(params string[] properties)
            where T : HubSpotCompany {
            EntityModel model = registry.Get(typeof(T));
            List<T> result=new List<T>();
            long? offset = null;
            do {
                JObject response = await rest.Get("companies/v2/companies/paged", GetListParameters(offset, properties).ToArray());

                foreach (JObject companyobject in response.GetValue("companies").OfType<JObject>())
                    result.Add(ToCompany<T>(companyobject, model));

                if (response.Value<bool>("has-more"))
                    offset = response.Value<long>("offset");
                else break;
            } while (true);

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
            JObject response = await rest.Delete($"companies/v2/companies/{id}");
            return ToCompany<T>(response, registry.Get(typeof(T)));
        }
    }
}
