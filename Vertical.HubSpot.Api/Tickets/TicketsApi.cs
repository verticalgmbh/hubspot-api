using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Vertical.HubSpot.Api.Extensions;
using Vertical.HubSpot.Api.Models;

namespace Vertical.HubSpot.Api.Tickets {

    /// <inheritdoc />
    public class TicketsApi : ITicketsApi {
        readonly HubSpotRestClient restclient;
        readonly ModelRegistry models;

        /// <summary>
        /// creates a new <see cref="TicketsApi"/>
        /// </summary>
        /// <param name="restclient">rest client used to access hubspot endpoints</param>
        /// <param name="models">entity model registry</param>
        internal TicketsApi(HubSpotRestClient restclient, ModelRegistry models) {
            this.restclient = restclient;
            this.models = models;
        }

        /// <inheritdoc />
        public async Task<T> Create<T>(T ticket) 
        where T : HubspotTicket {
            EntityModel ticketmodel = models.Get(typeof(T));

            JArray properties = new JArray();
            foreach (KeyValuePair<string, PropertyInfo> property in ticketmodel.Properties)
            {
                properties.Add(new JObject
                {
                    ["name"] = property.Key,
                    ["value"] = new JValue(property.Value.GetValue(ticket))
                });
            }

            JObject response=await restclient.Post<JObject>("crm-objects/v1/objects/tickets", properties);
            return response.ToEntity<T>(ticketmodel);
        }

        /// <inheritdoc />
        public async Task<T> GetTicket<T>(long ticketid)
        where T : HubspotTicket {
            EntityModel ticketmodel = models.Get(typeof(T));
            JObject response = await restclient.Get<JObject>($"crm-objects/v1/objects/tickets/{ticketid}");
            return response.ToEntity<T>(ticketmodel);
        }
    }
}