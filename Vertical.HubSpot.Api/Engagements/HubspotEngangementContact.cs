using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vertical.HubSpot.Api.Engagements
{
    public class HubspotEngangementContact
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("raw")]
        public string Raw { get; set; }
    }
}
