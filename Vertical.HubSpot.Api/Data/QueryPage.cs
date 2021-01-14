namespace Vertical.HubSpot.Api.Data {

    /// <summary>
    /// a page of query results
    /// </summary>
    public class QueryPage<T> {

        /// <summary>
        /// results of query
        /// </summary>
        public T[] Results { get; set; }

        /// <summary>
        /// paging information
        /// </summary>
        public Paging Paging { get; set; }
    }
}