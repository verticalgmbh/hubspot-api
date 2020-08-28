namespace Vertical.HubSpot.Api.Query {

    /// <summary>
    /// supported query filter operators
    /// </summary>
    public static class Operators {

        /// <summary>
        /// property is equal to a value
        /// </summary>
        public const string Equals = "EQ";

        /// <summary>
        /// property is not equal to a value
        /// </summary>
        public const string NotEquals = "NEQ";

        /// <summary>
        /// property is less than a value
        /// </summary>
        public const string Less = "LT";

        /// <summary>
        /// property is less or equal to a value
        /// </summary>
        public const string LessEqual = "LTE";

        /// <summary>
        /// property is greater than a value
        /// </summary>
        public const string Greater = "GT";

        /// <summary>
        /// property is greater or equal to a value
        /// </summary>
        public const string GreaterEqual = "GTE";

        /// <summary>
        /// property exists in object
        /// </summary>
        public const string HasProperty = "HAS_PROPERTY";

        /// <summary>
        /// property does not exist in object
        /// </summary>
        public const string NotHasProperty = "NOT_HAS_PROPERTY";

        /// <summary>
        /// object contains a token
        /// </summary>
        public const string ContainsToken = "CONTAINS_TOKEN";

        /// <summary>
        /// object does not contain a token
        /// </summary>
        public const string NotContainsToken = "NOT_CONTAINS_TOKEN";
    }
}