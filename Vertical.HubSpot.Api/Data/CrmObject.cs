using System;
using System.Collections.Generic;

namespace Vertical.HubSpot.Api.Data {

    /// <summary>
    /// object in crm
    /// </summary>
    public class CrmObject {

        /// <summary>
        /// object id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// property values
        /// </summary>
        public IDictionary<string, object> Properties { get; set; }

        /// <summary>
        /// date when object was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// date when object was updated
        /// </summary>
        public DateTime UpdatedAt { get; set; }


        /// <summary>
        /// indicates whether object is archived
        /// </summary>
        public bool Archived { get; set; }
    }
}