using System.Runtime.Serialization;

namespace Vertical.HubSpot.Api.Data {

    /// <summary>
    /// base object in hubspot
    /// </summary>
    public class HubspotObject {

        /// <summary>
        /// id of contact
        /// </summary>
        [IgnoreDataMember]
        [Name("objectId")]
        public long ID { get; set; }

        /// <summary>
        /// determines whether contact is deleted
        /// </summary>
        [IgnoreDataMember]
        [Name("isDeleted")]
        public bool IsDeleted { get; set; }

    }
}