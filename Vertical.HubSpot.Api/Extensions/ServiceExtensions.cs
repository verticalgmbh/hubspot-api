#if NETCOREAPP2_2
using Microsoft.Extensions.DependencyInjection;

namespace Vertical.HubSpot.Api.Extensions {


    public static class ServiceExtensions {
        
        // TODO: wip ... add configuration nuget just for netcoreapp target
        /*public static IServiceCollection AddHubspot(this IServiceCollection services)
        {
            services.AddSingleton<IHubSpot, HubSpot>(providers => {
                IConfiguration
                string apikey=providers.GetService<>()
            })
        }*/
    }

}
#endif