using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Vertical.HubSpot.Api;
using Vertical.HubSpot.Api.Contacts;
using Vertical.HubSpot.Test.Contacts;
using Xunit;

namespace Vertical.HubSpot.Test.Test
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

        protected Api.HubSpot GetHubSpot()
        {
            return new Api.HubSpot(new HubSpotOptions
            {
                ApiKey = Configuration["Hubspot:ApiKey"],
                Contact = new HubSpotContactOptions()
                {
                    IgnorePropertiesWithNullValues = true
                }
            });
        }
    }
}