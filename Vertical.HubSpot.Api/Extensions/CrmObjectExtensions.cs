using System;
using System.Collections.Generic;
using System.Reflection;
using NightlyCode.Core.Conversion;
using Vertical.HubSpot.Api.Data;
using Vertical.HubSpot.Api.Models;

namespace Vertical.HubSpot.Api.Extensions {

    /// <summary>
    /// extensions for <see cref="CrmObject"/>s
    /// </summary>
    static class CrmObjectExtensions {

        /// <summary>
        /// converts a crm object to an entity
        /// </summary>
        /// <typeparam name="T">type of entity to convert object to</typeparam>
        /// <param name="data">crm object to convert</param>
        /// <param name="model">model of object to convert to</param>
        /// <returns>created entity</returns>
        public static T Convert<T>(this CrmObject data, EntityModel model) {
            T result = Activator.CreateInstance<T>();
            if (model.IdProperty != null)
                model.IdProperty.SetValue(result, Converter.Convert(data.Id, model.IdProperty.PropertyType));

            foreach (KeyValuePair<string, object> property in data.Properties) {
                PropertyInfo propinfo = model.GetProperty(property.Key);
                if (propinfo == null)
                    continue;
                propinfo.SetValue(result, Converter.Convert(property.Value, propinfo.PropertyType));
            }

            return result;
        }
    }
}