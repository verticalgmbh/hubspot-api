namespace Vertical.HubSpot.Api.Associations {

    /// <summary>
    /// association in crm
    /// </summary>
    public class Association {

        /// <summary>
        /// id of connected object
        /// </summary>
        public long FromID { get; set; }

        /// <summary>
        /// id to which <see cref="FromID"/> object is connected
        /// </summary>
        public long ToID { get; set; }

        /// <summary>
        /// type of connection
        /// </summary>
        public AssociationType Type { get; set; }
    }
}