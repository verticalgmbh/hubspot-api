using System.Threading.Tasks;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.Contacts {

    /// <summary>
    /// access to contact api of hubspot
    /// </summary>
    public interface IContactApi {

        /// <summary>
        /// creates or updates a contact
        /// </summary>
        /// <typeparam name="T">type of contact model</typeparam>
        /// <param name="email">e-mail of contact to create or update</param>
        /// <param name="contact">contact data to create or update</param>
        /// <returns></returns>
        Task<long> CreateOrUpdate<T>(string email, T contact)
            where T : HubSpotContact;

        /// <summary>
        /// creates or updates a contact, without email
        /// </summary>
        /// <typeparam name="T">type of contact model</typeparam>
        /// <param name="contact">contact data to create or update</param>
        /// <returns></returns>
        Task<long> CreateOrUpdate<T>(T contact)
            where T : HubSpotContact;

        /// <summary>
        /// updates a contact
        /// </summary>
        /// <typeparam name="T">type of contact model</typeparam>
        /// <param name="contact">contact data to update</param>
        Task Update<T>(T contact)
            where T:HubSpotContact;

        /// <summary>
        /// deletes a contact
        /// </summary>
        /// <param name="id">id of contact</param>
        Task Delete(long id);

        /// <summary>
        /// lists a page of contacts
        /// </summary>
        /// <typeparam name="T">type of contact model</typeparam>
        /// <param name="offset">page offset</param>
        /// <param name="properties">properties to include in response</param>
        /// <returns>a page of contacts</returns>
        Task<PageResponse<T>> ListPage<T>(long? offset = null, int? count = null, params string[] properties)
            where T:HubSpotContact;

        /// <summary>
        /// get recently modified contacts
        /// </summary>
        /// <typeparam name="T">type of contact model</typeparam>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <returns>a page of recently modified contacts</returns>
        Task<PageResponse<T>> RecentlyUpdatedPage<T>(long? offset = null, int? count = null, params string[] properties)
            where T : HubSpotContact;

        /// <summary>
        /// get recently created contacts
        /// </summary>
        /// <typeparam name="T">type of contact model</typeparam>
        /// <param name="offset">offset to use to get a specific result page (optional)</param>
        /// <returns>a page of recently created contacts</returns>
        Task<PageResponse<T>> RecentlyCreatedPage<T>(long? offset = null, int? count = null, params string[] properties)
            where T : HubSpotContact;

        /// <summary>
        /// get a contact by id
        /// </summary>
        /// <typeparam name="T">type of contact model</typeparam>
        /// <param name="id">id of contact to return</param>
        /// <returns>contact data</returns>
        Task<T> Get<T>(long id)
            where T : HubSpotContact;

        /// <summary>
        /// get a contact by email
        /// </summary>
        /// <typeparam name="T">type of contact model</typeparam>
        /// <param name="email">email of contact to return</param>
        /// <returns>contact data</returns>
        Task<T> Get<T>(string email)
            where T : HubSpotContact;
    }
}