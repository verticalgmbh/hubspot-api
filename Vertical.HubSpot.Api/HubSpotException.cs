using System;
using Newtonsoft.Json.Linq;

namespace Vertical.HubSpot.Api {

    /// <summary>
    /// exception when communicating with hubspot
    /// </summary>
    public class HubSpotException : Exception {

        /// <summary>
        /// creates a new <see cref="HubSpotException"/>
        /// </summary>
        /// <param name="message">message describing error</param>
        /// <param name="request">original request sent to hubspot</param>
        public HubSpotException(string message, object request) 
            : this(message, null, request) {
        }

        /// <summary>
        /// creates a new <see cref="HubSpotException"/>
        /// </summary>
        /// <param name="message">message describing error</param>
        /// <param name="innerException">exception which is the source for this exception</param>
        /// <param name="request">original request sent to hubspot</param>
        public HubSpotException(string message, Exception innerException, object request) 
            : base(message, innerException) {
            Request = request;
        }

        /// <summary>
        /// original request to hubspot
        /// </summary>
        public object Request { get; }
    }
}