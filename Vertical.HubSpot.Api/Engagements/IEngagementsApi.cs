using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.Engagements
{

    public interface IEngagementsApi
    {

        Task<T> GetEngagement<T>(long id)
            where T : HubSpotEngagementResult;

        Task<JObject> DeleteEngagement(long id);

        Task<T> CreateEngagement<T>(HubSpotEngagementResult data)
            where T : HubSpotEngagementResult;

        Task<T> UpdateEngagement<T>(long id, HubSpotEngagementResult data)
            where T : HubSpotEngagementResult;

        /// <summary>
        /// creates or updates an engagement
        /// </summary>
        /// <typeparam name="T">type of engagement model</typeparam>
        /// <param name="type">type of the hubspot entity. Either company, contact or deal</param>
        /// <param name="contactId">id of contact to create or update</param>
        /// <returns>A list of engagements associated with the user</returns>
        Task<PageResponse<T>> ListAssociatedEngagements<T>(ObjectType type, long contactId, long? offset = null, int? count = null)
            where T : HubSpotEngagementResult;
        
            
    }
}