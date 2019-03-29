using System;

namespace Vertical.HubSpot.Api.Owners {

    /// <summary>
    /// owner in hubspot
    /// </summary>
    public class Owner {

        /// <summary>
        /// id of portal owner is registered to
        /// </summary>
        public long PortalId { get; set; }

        /// <summary>
        /// id of owner
        /// </summary>
        public long OwnerId { get; set; }

        /// <summary>
        /// type of owner
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// first name of owner
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// last name of owner
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// owner email
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// time when owner was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// time when owner was updated last
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// remote entries for owner
        /// </summary>
        public RemoteEntry RemoteList { get; set; }
    }
}