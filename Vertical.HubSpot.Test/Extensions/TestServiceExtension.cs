using Microsoft.Extensions.DependencyInjection;
using Vertical.HubSpot.Api;
using Vertical.HubSpot.Api.Contacts;
using Vertical.HubSpot.Api.Extensions;
using Vertical.HubSpot.Test.Test;
using Xunit;

namespace Vertical.HubSpot.Test.Extensions
{
    public class TestServiceExtension : TestBase
    {
        [Fact]
        public void TestHubSpotServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddHubspot(new HubSpotOptions
                {
                    ApiKey = Configuration["Hubspot:ApiKey"]
                })
                .BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var contactApi = scope.ServiceProvider.GetRequiredService<IContactApi>();
                Assert.NotNull(contactApi);
            }
        }
    }
}