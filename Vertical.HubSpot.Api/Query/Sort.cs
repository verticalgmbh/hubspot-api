namespace Vertical.HubSpot.Api.Query {

    /// <summary>
    /// sort criteria
    /// </summary>
    public class Sort {

        /// <summary>
        /// name of property to sort
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// direction of sort
        /// </summary>
        /// <remarks>
        /// only ASCENDING and DESCENDING are supported as values
        /// use constants in <see cref="SortDirection"/> for convenience
        /// </remarks>
        public string Direction { get; set; }
    }
}