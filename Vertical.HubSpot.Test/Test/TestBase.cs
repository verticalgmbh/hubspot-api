using Microsoft.Extensions.Configuration;

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
    }
}