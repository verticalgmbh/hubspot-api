using System.Runtime.Serialization;

namespace Vertical.HubSpot.Api.Companies {

    /// <summary>
    /// company in hubspot
    /// </summary>
    public class HubSpotCompany {

        /// <summary>
        /// id of company
        /// </summary>
        [IgnoreDataMember]
        public long ID { get; set; }

        /// <summary>
        /// determines whether the entity is deleted
        /// </summary>
        [IgnoreDataMember]
        public bool IsDeleted { get; set; }
    }
}