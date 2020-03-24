using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Vertical.HubSpot.Api;
using Vertical.HubSpot.Api.Contacts;
using Xunit;

namespace Vertical.HubSpot.UnitTest.Test
{
    public class TestBase
    {
        protected IConfigurationRoot Configuration { get; }

        public TestBase()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<TestBase>();

            Configuration = builder.Build();
        }

        protected Api.HubSpot GetHubSpotApi(HubSpotOptions options = null)
        {
            options = options ?? new HubSpotOptions();
            options.ApiKey = Configuration["Hubspot:ApiKey"];
            return new Api.HubSpot(options);
        }

        protected async Task<T> FindContactByEmail<T>(string email) where T : HubSpotContact
        {
            var hubspotApi = GetHubSpotApi();
            var contact = await hubspotApi.Contacts.Get<T>(email);
            Assert.True(contact != null, "Contact not found");
            return contact;
        }

    }
}