using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Linq;
using NightlyCode.Core.Conversion;
using Vertical.HubSpot.Api.Contacts;
using Vertical.HubSpot.Api.Models;

namespace Vertical.HubSpot.Api.Extensions
{

    /// <summary>
    /// extensions for data structures
    /// </summary>
    static class DataExtensions {
        static DateTime unixstart = new DateTime(1970, 1, 1);

        /// <summary>
        /// converts a json response to a <see cref="HubSpotContact"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contact">json containing contact data</param>
        /// <param name="model">model for contact entity</param>
        /// <returns></returns>
        public static T ToContact<T>(this JObject contact, EntityModel model)
            where T : HubSpotContact
        {
            T result = Activator.CreateInstance<T>();
            result.ID = contact.Value<long>("vid");
            result.Deleted = contact.Value<bool>("Deleted");

            JObject responseproperties = (JObject)contact["properties"];
            foreach (KeyValuePair<string, JToken> property in responseproperties) {
                PropertyInfo objectproperty = model.GetProperty(property.Key);
                if (objectproperty == null)
                    continue;
                objectproperty.SetValue(result, Converter.Convert(property.Value.Value<object>("value"), objectproperty.PropertyType));
            }

            return result;
        }

        /// <summary>
        /// converts a datetime to a unix timestamp
        /// </summary>
        /// <param name="date">date to be converted</param>
        /// <returns>milliseconds which passed since 1970-01-01</returns>
        public static long ToUnixTimestamp(this DateTime date) {

            return (long) (date - unixstart).TotalMilliseconds;
        }
    }
}
