using System.Threading.Tasks;

namespace Vertical.HubSpot.Api.Tickets {

    /// <summary>
    /// interacts with tickets on hubspot
    /// </summary>
    public interface ITicketsApi {

        /// <summary>
        /// creates a ticket in hubspot
        /// </summary>
        /// <typeparam name="T">entity type of ticket to create</typeparam>
        /// <param name="ticket">ticket entity to create</param>
        /// <returns>created ticket</returns>
        Task<T> Create<T>(T ticket) where T : HubspotTicket;

        /// <summary>
        /// reads ticket information from hubspot using a ticket id
        /// </summary>
        /// <typeparam name="T">entity type of ticket to return</typeparam>
        /// <param name="ticketid">id of ticket to get</param>
        /// <returns>fetched ticket data</returns>
        Task<T> GetTicket<T>(long ticketid) where T : HubspotTicket;
    }
}