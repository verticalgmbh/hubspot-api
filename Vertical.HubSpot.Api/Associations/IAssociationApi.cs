using System.Threading.Tasks;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.Associations {
    /// <summary>
    /// access to assocations api of hubspot
    /// </summary>
    public interface IAssociationApi {

        /// <summary>
        /// Associate 2 CRM objects. You would use this endpoint to associate a ticket with a contact, or to associate a line item object to a deal
        /// </summary>
        /// <param name="fromid">The ID of the object being associated</param>
        /// <param name="toid">The ID of the object the from object is being associated with</param>
        /// <param name="type">The ID of the association definition</param>
        Task Create(long fromid, long toid, AssociationType type);

        /// <summary>
        /// creates multiple associations at once
        /// </summary>
        /// <param name="associations">associations to create</param>
        Task Create(params Association[] associations);

        /// <summary>
        /// Get the IDs of objects associated with the given object, based on the specified association type
        /// </summary>
        /// <param name="objectid">id of which to list associations</param>
        /// <param name="type">type of assocation to list</param>
        /// <param name="offset">page token used to get next page in multipage result (optional)</param>
        /// <returns>one page of list results</returns>
        Task<PageResponse<long>> ListPage(long objectid, AssociationType type, long? offset=null);

        /// <summary>
        /// Get the IDs of objects associated with the given object, based on the specified association type
        /// </summary>
        /// <param name="objectid">id of which to list associations</param>
        /// <param name="type">type of assocation to list</param>
        /// <returns>list results</returns>
        /// <returns>all results of the specified type</returns>
        Task<long[]> List(long objectid, AssociationType type);

        /// <summary>
        /// Delete an association between 2 CRM objects
        /// </summary>
        /// <param name="fromid">The ID of the object you want to remove the association from</param>
        /// <param name="toid">The ID of the currently associated object that you're removing the association from</param>
        /// <param name="type">The ID of the association definition</param>
        Task Delete(long fromid, long toid, AssociationType type);

        /// <summary>
        /// The JSON data in the PUT request will be a list of items each representing a single association that you want to delete
        /// </summary>
        /// <param name="associations">associations to delete</param>
        Task Delete(params Association[] associations);
    }
}