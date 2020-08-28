using Vertical.HubSpot.Api.Contacts;

namespace Vertical.HubSpot.Api.Tests.Data {
    public class TestContact : HubSpotContact{
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}