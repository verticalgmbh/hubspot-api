using System;

namespace Vertical.HubSpot.Api.Logging
{

    /// <summary>
    /// logic used to initialize logging methods
    /// </summary>
    public static class LogSetup
    {

        /// <summary>
        /// logic used to initialize logging methods
        /// </summary>
        public static void SetInfo(Action<object, string, string> logger) {
            Logger.info = logger;
        }

        /// <summary>
        /// logic used to initialize logging methods
        /// </summary>
        public static void SetWarning(Action<object, string, string> logger) {
            Logger.warning = logger;
        }

        /// <summary>
        /// logic used to initialize logging methods
        /// </summary>
        public static void SetError(Action<object, string, Exception> logger) {
            Logger.error = logger;
        }
    }
}
