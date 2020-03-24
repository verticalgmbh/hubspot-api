using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Vertical.HubSpot.Api.Models {

    /// <summary>
    /// model for an entity
    /// </summary>
#if DEBUG
    public
#endif
    class EntityModel {
        readonly Dictionary<string, PropertyInfo> propertymapping=new Dictionary<string, PropertyInfo>();
        readonly Dictionary<string, string> namemapping = new Dictionary<string, string>();

        /// <summary>
        /// all properties in entity model
        /// </summary>
        public IEnumerable<KeyValuePair<string, PropertyInfo>> Properties => propertymapping;

        /// <summary>
        /// adds a property to the model
        /// </summary>
        /// <param name="property">property information</param>
        /// <param name="mapping">mapping name</param>
        public void AddProperty(PropertyInfo property, string mapping) {
            propertymapping[mapping] = property;
            namemapping[property.Name] = mapping;
        }

        /// <summary>
        /// get property information for a hubspot name
        /// </summary>
        /// <param name="mapping">name of mapping in hubspot</param>
        /// <returns>property information to use</returns>
        public PropertyInfo GetProperty(string mapping) {
            propertymapping.TryGetValue(mapping, out PropertyInfo property);
            return property;
        }

        /// <summary>
        /// get mapping for a property
        /// </summary>
        /// <param name="property">name of property</param>
        /// <returns>mapping to use in hubspot</returns>
        public string GetMapping(string property) {
            namemapping.TryGetValue(property, out string mapping);
            return mapping;
        }
    }
}