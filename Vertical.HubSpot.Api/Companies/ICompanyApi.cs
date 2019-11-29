using System.Threading.Tasks;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.Companies {

    /// <summary>
    /// access to company api of hubspot
    /// </summary>
    public interface ICompanyApi {

        /// <summary>
        /// creates a new company in hubspot
        /// </summary>
        /// <typeparam name="T">type of company</typeparam>
        /// <param name="company">company data to create</param>
        /// <returns>created entity</returns>
        Task<T> Create<T>(T company)
            where T : HubSpotCompany;

        /// <summary>
        /// updates a company in hubspot
        /// </summary>
        /// <typeparam name="T">type of company</typeparam>
        /// <param name="company">company data to update</param>
        /// <returns>updated company</returns>
        Task<T> Update<T>(T company)
            where T : HubSpotCompany;

        /// <summary>
        /// updates several companies in hubspot
        /// </summary>
        /// <typeparam name="T">type of company model</typeparam>
        /// <param name="companies">data for companies to update</param>
        Task BatchUpdate<T>(params T[] companies)
            where T : HubSpotCompany;

        /// <summary>
        /// lists all companies and returns a page of the result
        /// </summary>
        /// <typeparam name="T">type of company model</typeparam>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <param name="properties">properties to include in result</param>
        /// <returns>one page of company list response</returns>
        Task<PageResponse<T>> ListPage<T>(long? offset=null, params string[] properties)
            where T:HubSpotCompany;

        /// <summary>
        /// get recently modified companies
        /// </summary>
        /// <typeparam name="T">type of company model</typeparam>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <returns>a page of recently modified companies</returns>
        Task<PageResponse<T>> RecentlyModifiedPage<T>(long? offset = null)
            where T : HubSpotCompany;

        /// <summary>
        /// get recently created companies
        /// </summary>
        /// <typeparam name="T">type of company model</typeparam>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <returns>a page of recently created companies</returns>
        Task<PageResponse<T>> RecentlyCreatedPage<T>(long? offset = null)
            where T : HubSpotCompany;

        /// <summary>
        /// searches for companies by domain criteria
        /// </summary>
        /// <typeparam name="T">type of company model</typeparam>
        /// <param name="domain">domain to search for</param>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <param name="properties">properties to include in search result</param>
        /// <returns>a page of recently created companies</returns>
        Task<PageResponse<T>> SearchByDomainPage<T>(string domain, long? offset = null, params string[] properties)
            where T : HubSpotCompany;

        /// <summary>
        /// lists all companies
        /// </summary>
        /// <typeparam name="T">type of company to return</typeparam>
        /// <param name="properties">properties to include in result</param>
        /// <returns>list of all companies</returns>
        Task<T[]> List<T>(params string[] properties)
            where T : HubSpotCompany;

        /// <summary>
        /// deletes a company from hubspot
        /// </summary>
        /// <typeparam name="T">type of company to delete</typeparam>
        /// <param name="id">company id</param>
        /// <returns>deleted company</returns>
        Task<T> Delete<T>(long id)
            where T : HubSpotCompany;

        /// <summary>
        /// get a company by it's id
        /// </summary>
        /// <typeparam name="T">type of company model</typeparam>
        /// <param name="id">id of company to return</param>
        /// <returns>company data</returns>
        Task<T> Get<T>(long id)
            where T : HubSpotCompany;
    }
}