#if NETSTANDARD
using System;
using Microsoft.Extensions.DependencyInjection;
using Vertical.HubSpot.Api.Associations;
using Vertical.HubSpot.Api.Companies;
using Vertical.HubSpot.Api.Contacts;
using Vertical.HubSpot.Api.Deals;
using Vertical.HubSpot.Api.Tickets;

namespace Vertical.HubSpot.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddHubspot(this IServiceCollection services, string apiKey)
        {
            if( string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentNullException(nameof(apiKey));

            return AddHubspot( services, new HubSpotOptions
            {
                ApiKey = apiKey
            });
        }

        public static IServiceCollection AddHubspot(this IServiceCollection services, HubSpotOptions options)
        {
            if (string.IsNullOrWhiteSpace(options?.ApiKey)) throw new ArgumentNullException(nameof(HubSpotOptions.ApiKey));
            if (string.IsNullOrWhiteSpace(options?.ApiUrl)) throw new ArgumentNullException(nameof(HubSpotOptions.ApiUrl));

            return services
                .AddSingleton<IHubSpot, HubSpot>(provider => new HubSpot(options))
                .AddSingleton<IAssociationApi, AssociationApi>(provider =>
                {
                    var hubspot = provider.GetRequiredService<IHubSpot>();
                    return hubspot.Associations as AssociationApi ?? throw new NotSupportedException(nameof(IAssociationApi));
                })
                .AddSingleton<ICompanyApi, CompanyApi>(provider =>
                {
                    var hubspot = provider.GetRequiredService<IHubSpot>();
                    return hubspot.Companies as CompanyApi ?? throw new NotSupportedException(nameof(ICompanyApi));
                })
                .AddSingleton<IContactApi, ContactApi>(provider =>
                {
                    var hubspot = provider.GetRequiredService<IHubSpot>();
                    return hubspot.Contacts as ContactApi ?? throw new NotSupportedException(nameof(IContactApi));
                })
                .AddSingleton<IDealsApi, DealsApi>(provider =>
                {
                    var hubspot = provider.GetRequiredService<IHubSpot>();
                    return hubspot.Deals as DealsApi ?? throw new NotSupportedException(nameof(IDealsApi));
                })
                .AddSingleton<ITicketsApi, TicketsApi>(provider =>
                {
                    var hubspot = provider.GetRequiredService<IHubSpot>();
                    return hubspot.Tickets as TicketsApi ?? throw new NotSupportedException(nameof(ITicketsApi));
                });
        }
    }

}
#endif