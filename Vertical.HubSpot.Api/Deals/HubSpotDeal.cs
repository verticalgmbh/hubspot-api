using System.Runtime.Serialization;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.Deals {

    /// <summary>
    /// deal in hubspot
    /// </summary>
    public class HubSpotDeal {

        /// <summary>
        /// id of company
        /// </summary>
        [IgnoreDataMember]
        [HubspotId]
        public long ID { get; set; }

        /// <summary>
        /// determines whether the entity is deleted
        /// </summary>
        [IgnoreDataMember]
        [HubspotDeleted]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// associated companies
        /// </summary>
        [IgnoreDataMember]
        public long[] Companies { get; set; }

        /// <summary>
        /// associated contacts
        /// </summary>
        [IgnoreDataMember]
        public long[] Contacts { get; set; }

        /// <summary>
        /// associated deals
        /// </summary>
        [IgnoreDataMember]
        public long[] Deals { get; set; }

        /// <summary>
        /// stage of deal
        /// </summary>
        public string DealStage { get; set; }
    }
}