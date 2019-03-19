using System;

namespace Vertical.HubSpot.Api.Logging
{

    /// <summary>
    /// structure used for logging
    /// </summary>
    class Logger
    {
        internal static Action<object, string, string> info = (s, m, d) => { };

        internal static Action<object, string, string> warning = (s, m, d) => { };

        internal static Action<object, string, Exception> error = (s, m, d) => { };

        /// <summary>
        /// logs an info
        /// </summary>
        /// <param name="sender">log sender</param>
        /// <param name="message">message to log</param>
        /// <param name="details">details for message</param>
        public static void Info(object sender, string message, string details = null)
        {
            info(sender, message, details);
        }

        /// <summary>
        /// logs a warning
        /// </summary>
        /// <param name="sender">log sender</param>
        /// <param name="message">message to log</param>
        /// <param name="details">details for message</param>
        public static void Warning(object sender, string message, string details = null)
        {
            warning(sender, message, details);
        }

        /// <summary>
        /// logs an error
        /// </summary>
        /// <param name="sender">log sender</param>
        /// <param name="message">message to log</param>
        /// <param name="details">error details</param>
        public static void Error(object sender, string message, Exception details = null)
        {
            error(sender, message, details);
        }
    }
}
