using System;

namespace Vertical.HubSpot.Api {

    /// <summary>
    /// exception when communicating with hubspot
    /// </summary>
    public class HubSpotException : Exception {

        /// <summary>
        /// creates a new <see cref="HubSpotException"/>
        /// </summary>
        /// <param name="message">message describing error</param>
        public HubSpotException(string message) : base(message) {
        }

        /// <summary>
        /// creates a new <see cref="HubSpotException"/>
        /// </summary>
        /// <param name="message">message describing error</param>
        /// <param name="innerException">exception which is the source for this exception</param>
        public HubSpotException(string message, Exception innerException) 
            : base(message, innerException) {
        }
    }
}