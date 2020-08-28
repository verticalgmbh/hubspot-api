using System.Globalization;

namespace Vertical.HubSpot.Api.Query {

    /// <summary>
    /// query object to be used for query calls
    /// </summary>
    public class ObjectQuery {

        /// <summary>
        /// limit for result
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// token which specifies where to begin listing results
        /// </summary>
        public string After { get; set; }

        /// <summary>
        /// properties to include in result
        /// </summary>
        public string[] Properties { get; set; }

        /// <summary>
        /// sort criterias
        /// </summary>
        public Sort[] Sorts { get; set; }

        /// <summary>
        /// fulltext search over all properties
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// specifies a single filter group to apply
        /// </summary>
        /// <remarks>
        /// filters are automatically concatenated using AND
        /// property filters and filtergroups are mutually exclusive
        /// </remarks>
        public Filter[] Filters { get; set; }

        /// <summary>
        /// specifies multiple filters to connect using OR
        /// </summary>
        /// <remarks>
        /// property filters and filtergroups are mutually exclusive
        /// Groups are concatenated using OR
        /// </remarks>
        public FilterGroup[] FilterGroups { get; set; }
    }
}