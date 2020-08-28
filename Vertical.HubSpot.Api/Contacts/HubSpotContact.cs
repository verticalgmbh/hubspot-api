using System.Runtime.Serialization;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.Contacts
{

    /// <summary>
    /// base information for a contact in hubspot
    /// </summary>
    public class HubSpotContact
    {

        /// <summary>
        /// id of contact
        /// </summary>
        [IgnoreDataMember]
        [Name("vid")]
        [HubspotId]
        public long ID { get; set; }

        /// <summary>
        /// determines whether contact is deleted
        /// </summary>
        [IgnoreDataMember]
        public bool Deleted { get; set; }
    }
}
