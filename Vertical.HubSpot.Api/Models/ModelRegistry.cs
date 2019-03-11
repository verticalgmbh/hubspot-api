using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Vertical.HubSpot.Api.Data;

namespace Vertical.HubSpot.Api.Models {

    /// <summary>
    /// registry for models of entities
    /// </summary>
    class ModelRegistry {
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
                        if (Attribute.IsDefined(property, typeof(IgnoreDataMemberAttribute)))
                            continue;

                        string mappingname = property.Name.ToLower();
                        NameAttribute name = Attribute.GetCustomAttribute(property, typeof(NameAttribute)) as NameAttribute;
                        if (name != null)
                            mappingname = name.Name;
                        model.AddProperty(property, mappingname);
                    }

                    models[entitytype] = model;
                }
            }

            return model;
        }
    }
}