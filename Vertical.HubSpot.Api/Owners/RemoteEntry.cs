namespace Vertical.HubSpot.Api.Owners {

    /// <summary>
    /// TODO: sadly hubspot does not explain what remote means
    /// </summary>
    public class RemoteEntry {

        /// <summary>
        /// id of portal
        /// </summary>
        public long PortalId { get; set; }

        /// <summary>
        /// id of owner
        /// </summary>
        public long OwnerId { get; set; }

        /// <summary>
        /// id of remote entry
        /// </summary>
        public string RemoteId { get; set; }

        /// <summary>
        /// usually "Hubspot"
        /// </summary>
        public string RemoteType { get; set; }

        /// <summary>
        /// determines whether entry is active
        /// </summary>
        public bool Active { get; set; }
    }
}