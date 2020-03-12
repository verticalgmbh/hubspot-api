using Vertical.HubSpot.Api.Contacts;

namespace Vertical.HubSpot.Api
{
    public class HubSpotOptions
    {
        public string ApiKey { get; set; }
        public string ApiUrl { get; set; } = "https://api.hubapi.com/";

        public HubSpotContactOptions Contact { get; set; } = new HubSpotContactOptions();
    }
}