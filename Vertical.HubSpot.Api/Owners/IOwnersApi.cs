using System.Threading.Tasks;

namespace Vertical.HubSpot.Api.Owners
{
    public interface IOwnersApi
    {
        Task<HubSpotOwner> Get<T>(long id) where T : HubSpotOwner;
        Task<HubSpotOwner[]> List(string email = null, bool includeinactive = false);
    }
}