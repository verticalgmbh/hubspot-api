using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Vertical.HubSpot.Api.Data;
using Vertical.HubSpot.Api.Extensions;
using Vertical.HubSpot.Api.Models;

namespace Vertical.HubSpot.Api.Contacts {

    /// <summary>
    /// access to contacts api
    /// </summary>
    public class ContactApi {
        readonly HubSpotRestClient rest;
        readonly ModelRegistry models;

        /// <summary>
        /// creates a new <see cref="ContactApi"/>
        /// </summary>
        /// <param name="rest">rest client to call api</param>
        /// <param name="models">model registry used to access entity models</param>
        internal ContactApi(HubSpotRestClient rest, ModelRegistry models) {
            this.rest = rest;
            this.models = models;
        }

        IEnumerable<Parameter> GetListParameters(long? offset, params string[] properties)
        {
            yield return new Parameter("limit", "100");
            if (offset.HasValue)
                yield return new Parameter("offset", offset.ToString());
            foreach (string property in properties)
                yield return new Parameter("properties", property);
        }

        /// <summary>
        /// creates or updates a contact
        /// </summary>
        /// <typeparam name="T">type of contact model</typeparam>
        /// <param name="email">e-mail of contact to create or update</param>
        /// <param name="contact">contact data to create or update</param>
        /// <returns></returns>
        public async Task<long> CreateOrUpdate<T>(string email, T contact)
            where T : HubSpotContact {
            EntityModel model = models.Get(typeof(T));

            JObject request=new JObject();
            JArray properties=new JArray();
            foreach (KeyValuePair<string, PropertyInfo> property in model.Properties) {
                properties.Add(new JObject {
                    ["property"] = property.Key,
                    ["value"] = new JValue(property.Value.GetValue(contact))
                });
            }
            request["properties"] = properties;

            JObject response = await rest.Post<JObject>($"contacts/v1/contact/createOrUpdate/email/{email}", request);
            return response.Value<long>("vid");
        }

        /// <summary>
        /// updates a contact
        /// </summary>
        /// <typeparam name="T">type of contact model</typeparam>
        /// <param name="contact">contact data to update</param>
        public async Task Update<T>(T contact)
            where T:HubSpotContact
        {
            EntityModel model = models.Get(typeof(T));

            JObject request = new JObject();
            JArray properties = new JArray();
            foreach (KeyValuePair<string, PropertyInfo> property in model.Properties)
            {
                properties.Add(new JObject
                {
                    ["property"] = property.Key,
                    ["value"] = new JValue(property.Value.GetValue(contact))
                });
            }
            request["properties"] = properties;

            await rest.Post<JObject>($"contacts/v1/contact/vid/{contact.ID}/profile", request);
        }

        /// <summary>
        /// deletes a contact
        /// </summary>
        /// <param name="id">id of contact</param>
        public async Task Delete(long id) {
            await rest.Delete<JObject>($"contacts/v1/contact/vid/{id}");
        }

        /// <summary>
        /// lists a page of contacts
        /// </summary>
        /// <typeparam name="T">type of contact model</typeparam>
        /// <param name="offset">page offset</param>
        /// <param name="properties">properties to include in response</param>
        /// <returns>a page of contacts</returns>
        public async Task<PageResponse<T>> ListPage<T>(long? offset = null, params string[] properties)
            where T:HubSpotContact
        {
            EntityModel model = models.Get(typeof(T));

            JObject response = await rest.Get<JObject>("contacts/v1/lists/all/contacts/all", GetListParameters(offset, properties).ToArray());

            return new PageResponse<T>
            {
                Offset = response.Value<bool>("has-more") ? response.Value<long?>("vid-offset") : null,
                Data = response.GetValue("contacts").OfType<JObject>().Select(d => d.ToContact<T>(model)).ToArray()
            };
        }

        /// <summary>
        /// get a contact by id
        /// </summary>
        /// <typeparam name="T">type of contact model</typeparam>
        /// <param name="id">id of contact to return</param>
        /// <returns>contact data</returns>
        public async Task<T> Get<T>(long id)
            where T : HubSpotContact {
            JObject response = await rest.Get<JObject>($"contacts/v1/contact/vid/{id}/profile");
            EntityModel model = models.Get(typeof(T));
            return response.ToContact<T>(model);
        }
    }
}