using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Vertical.HubSpot.Api.Data {
    /// <summary>
    /// parameters used in query strings
    /// </summary>
    public class QueryParameters {
        readonly List<Parameter> parameters = new List<Parameter>();

        /// <summary>
        /// creates new <see cref="QueryParameters"/>
        /// </summary>
        /// <param name="parameters">parameters to include</param>
        public QueryParameters(params Parameter[] parameters) {
            this.parameters.AddRange(parameters);
        }

        /// <summary>
        /// creates new <see cref="QueryParameters"/> with a single parameter contained
        /// </summary>
        /// <param name="name">name of parameter</param>
        /// <param name="value">parameter value</param>
        public QueryParameters(string name, string value) {
            parameters.Add(new Parameter(name, value));
        }

        /// <summary>
        /// adds a parameter to the query parameter collection
        /// </summary>
        /// <param name="parameter">parameter to add</param>
        public void Add(Parameter parameter) {
            parameters.Add(parameter);
        }

        /// <summary>
        /// adds a parameter to the query parameter collection
        /// </summary>
        /// <param name="name">name of parameter to add</param>
        /// <param name="value">value of parameter to add</param>
        public void Add(string name, string value) {
            if (value == null)
                return;
            if (value is string stringvalue && stringvalue == "")
                return;

            Add(new Parameter(name, value));
        }

        /// <inheritdoc />
        public override string ToString() {
            if (parameters.Count == 0)
                return "";

            StringBuilder sb = new StringBuilder("?");
            foreach (Parameter parameter in parameters)
                sb.Append(WebUtility.UrlEncode(parameter.Key)).Append('=').Append(WebUtility.UrlEncode(parameter.Value)).Append('&');
            --sb.Length;
            return sb.ToString();
        }
    }
}