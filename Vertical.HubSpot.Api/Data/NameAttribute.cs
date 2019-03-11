using System;

namespace Vertical.HubSpot.Api.Data {

    /// <summary>
    /// attribute specifying how to map the property to an internal entity name
    /// </summary>
    public class NameAttribute : Attribute {

        /// <summary>
        /// creates a new <see cref="NameAttribute"/>
        /// </summary>
        /// <param name="name">name to map property to</param>
        public NameAttribute(string name) {
            Name = name;
        }

        /// <summary>
        /// internal name of entity-property in hubspot
        /// </summary>
        public string Name { get; }
    }
}