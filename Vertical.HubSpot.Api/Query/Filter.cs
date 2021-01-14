namespace Vertical.HubSpot.Api.Query {
    /// <summary>
    /// a filter for a property
    /// </summary>
    public class Filter {

        /// <summary>
        /// property to filter for
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// operator to apply
        /// </summary>
        /// <remarks>
        /// use <see cref="Operators"/> for convenience
        /// </remarks>
        public string Operator { get; set; }

        /// <summary>
        /// value to filter for
        /// </summary>
        public object Value { get; set; }
    }
}