using System.Runtime.Serialization;

namespace Vertical.HubSpot.Api.Deals {

    /// <summary>
    /// deal in hubspot
    /// </summary>
    public class HubSpotDeal {

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

        /// <summary>
        /// associated companies
        /// </summary>
        [IgnoreDataMember]
        public long[] Companies { get; set; } = new long[] { };

        /// <summary>
        /// associated contacts
        /// </summary>
        [IgnoreDataMember]
        public long[] Contacts { get; set; } = new long[] { };

        /// <summary>
        /// associated deals
        /// </summary>
        [IgnoreDataMember]
        public long[] Deals { get; set; } = new long[] { };

        /// <summary>
        /// stage of deal
        /// </summary>
        public string DealStage { get; set; }
    }
}