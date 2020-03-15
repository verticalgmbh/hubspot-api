namespace Vertical.HubSpot.Api.Data {

    /// <summary>
    /// response containing a page of data
    /// </summary>
    /// <typeparam name="T">type of response data</typeparam>
    public class PageResponse<T> {

        /// <summary>
        /// result data
        /// </summary>
        public T[] Data { get; set; }

        /// <summary>
        /// offset to use to get next page if more data is available
        /// </summary>
        public long? Offset { get; set; }

        [Name("has-more")]
        public bool? HasMore { get; set; }
    }
}