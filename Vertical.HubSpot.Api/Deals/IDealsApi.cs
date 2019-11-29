using System;
using System.Threading.Tasks;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.Deals {

    /// <summary>
    /// access to deals in hubspot
    /// </summary>
    public interface IDealsApi {

        /// <summary>
        /// creates a new deal in hubspot
        /// </summary>
        /// <typeparam name="T">type of deal to create</typeparam>
        /// <param name="dealdata">data of deal to create</param>
        /// <returns>created deal</returns>
        Task<T> Create<T>(T dealdata)
            where T : HubSpotDeal;

        /// <summary>
        /// updates properties of a deal
        /// </summary>
        /// <typeparam name="T">type of deal model to update</typeparam>
        /// <param name="dealdata">data to update</param>
        /// <returns>updated deal</returns>
        Task<T> Update<T>(T dealdata)
            where T : HubSpotDeal;

        /// <summary>
        /// updates multiple deals in a batch request
        /// </summary>
        /// <typeparam name="T">type of deal model</typeparam>
        /// <param name="deals">deals to update</param>
        Task Update<T>(T[] deals)
            where T : HubSpotDeal;

        /// <summary>
        /// lists all deals and returns a page of the result
        /// </summary>
        /// <typeparam name="T">type of deal model</typeparam>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <param name="properties">properties to include in result</param>
        /// <returns>one page of deal list response</returns>
        Task<PageResponse<T>> ListPage<T>(long? offset = null, params string[] properties)
            where T : HubSpotDeal;

        /// <summary>
        /// lists all deals
        /// </summary>
        /// <typeparam name="T">type of deal to return</typeparam>
        /// <param name="properties">properties to include in result</param>
        /// <returns>list of all deals</returns>
        Task<T[]> List<T>(params string[] properties)
            where T : HubSpotDeal;

        /// <summary>
        /// get recently modified deals
        /// </summary>
        /// <typeparam name="T">type of deal model</typeparam>
        /// <param name="since">only returns deals modified since this date</param>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <returns>a page of recently modified deals</returns>
        Task<PageResponse<T>> RecentlyModifiedPage<T>(DateTime? since, long? offset = null)
            where T : HubSpotDeal;

        /// <summary>
        /// get recently created deals
        /// </summary>
        /// <typeparam name="T">type of deal model</typeparam>
        /// <param name="since">only returns deals modified since this date</param>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <returns>a page of recently modified deals</returns>
        Task<PageResponse<T>> RecentlyCreatedPage<T>(DateTime? since, long? offset = null)
            where T : HubSpotDeal;

        /// <summary>
        /// deletes a deal from hubspot
        /// </summary>
        /// <typeparam name="T">type of deal to delete</typeparam>
        /// <param name="id">deal id</param>
        /// <returns>deleted deal</returns>
        Task<T> Delete<T>(long id)
            where T : HubSpotDeal;

        /// <summary>
        /// get a deal by it's id
        /// </summary>
        /// <typeparam name="T">type of deal model</typeparam>
        /// <param name="id">id of deal to return</param>
        /// <returns>deal data</returns>
        Task<T> Get<T>(long id)
            where T : HubSpotDeal;
    }
}