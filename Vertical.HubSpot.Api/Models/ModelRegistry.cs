using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.Models {

    /// <summary>
    /// registry for models of entities
    /// </summary>
#if DEBUG
    public
#endif
    class ModelRegistry
    {
        private static readonly Type[] IgnoreAttributes = { typeof(IgnoreDataMemberAttribute), typeof(JsonIgnoreAttribute) };

        readonly Dictionary<Type, EntityModel> models=new Dictionary<Type, EntityModel>();
        readonly object modellock = new object();

        /// <summary>
        /// get the entitymodel for a type
        /// </summary>
        /// <param name="entitytype">type of entity for which to get a model</param>
        /// <returns>entity model for the specified entity type</returns>
        public EntityModel Get(Type entitytype) {
            EntityModel model;
            lock (modellock) {
                if (!models.TryGetValue(entitytype, out model)) {
                    model = new EntityModel();
                    foreach (PropertyInfo property in entitytype.GetProperties()) {
                        if(IgnoreAttributes.Any(t=> Attribute.IsDefined(property, t)) )
                            continue;

                        string mappingname = property.Name.ToLower();
                        if (Attribute.GetCustomAttribute(property, typeof(NameAttribute)) is NameAttribute name)
                            mappingname = name.Name;
                        else if(Attribute.GetCustomAttribute(property, typeof(JsonPropertyAttribute)) is JsonPropertyAttribute jsonProperty)
                            mappingname = jsonProperty.PropertyName;

                        model.AddProperty(property, mappingname);
                    }

                    models[entitytype] = model;
                }
            }

            return model;
        }
    }
}