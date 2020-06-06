using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Linq;
using NightlyCode.Core.Conversion;
using Vertical.HubSpot.Api.BlogPost;
using Vertical.HubSpot.Api.Contacts;
using Vertical.HubSpot.Api.Data;
using Vertical.HubSpot.Api.Models;
using Vertical.HubSpot.Api.Owners;

namespace Vertical.HubSpot.Api.Extensions
{

    /// <summary>
    /// extensions for data structures
    /// </summary>
    static class DataExtensions {
        static readonly DateTime unixstart = new DateTime(1970, 1, 1);

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
        /// converts a json response to a <see cref="HubSpotOwner"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="owner">json containing owner data</param>
        /// <param name="model">model for owner entity</param>
        /// <returns></returns>
        public static T ToOwner<T>(this JObject owner)
            where T : HubSpotOwner
        {
            T result = Activator.CreateInstance<T>();
            result.OwnerId = owner.Value<long>("ownerId");
            result.PortalId = owner.Value<long>("portalId");
            result.Type = owner.Value<string>("type");
            result.FirstName = owner.Value<string>("firstName");
            result.LastName = owner.Value<string>("lastName");
            result.Email = owner.Value<string>("email");

            // todo deserialise remoteentry
            result.CreatedAt = (DateTime)Converter.Convert(owner["createdAt"], typeof(DateTime));
            result.UpdatedAt = (DateTime)Converter.Convert(owner["updatedAt"], typeof(DateTime));

            return result;
        }

        /// <summary>
        /// converts a json response to a <see cref="HubSpotContact"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contact">json containing contact data</param>
        /// <param name="model">model for contact entity</param>
        /// <returns></returns>
        public static T ToEntity<T>(this JObject contact, EntityModel model)
        where T : HubspotObject
        {
            T result = Activator.CreateInstance<T>();

            // TODO: add these properties to model and flag them as not to be posted on requests
            result.ID = contact.Value<long>("objectId");
            result.IsDeleted = contact.Value<bool>("isDeleted");

            JObject responseproperties = (JObject)contact["properties"];
            foreach (KeyValuePair<string, JToken> property in responseproperties)
            {
                PropertyInfo objectproperty = model.GetProperty(property.Key);
                if (objectproperty == null)
                    continue;
                objectproperty.SetValue(result, Converter.Convert(property.Value.Value<object>("value"), objectproperty.PropertyType));
            }

            return result;
        }

        /// <summary>
        /// converts a json response to a <see cref="HubSpotBlogPost"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="blogpost">json containing contact data</param>
        /// <param name="model">model for contact entity</param>
        /// <returns></returns>
        public static T ToBlogPost<T>(this JObject blogpost, EntityModel model) where T : HubSpotBlogPost
        {
            T result = Activator.CreateInstance<T>();

//            JObject responseproperties = (JObject)blogpost["properties"];
            foreach (KeyValuePair<string, JToken> property in blogpost)
            {
                PropertyInfo objectproperty = model.GetProperty(property.Key);
                if (objectproperty == null)
                    continue;
                objectproperty.SetValue(result, Converter.Convert(property.Value.Value<object>(), objectproperty.PropertyType));
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
