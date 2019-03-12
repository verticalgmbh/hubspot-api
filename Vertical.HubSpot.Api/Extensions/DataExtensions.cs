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
    static class DataExtensions
    {

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

    }
}
