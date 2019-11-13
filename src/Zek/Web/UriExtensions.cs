using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using Zek.Utils;

namespace Zek.Web
{
    public static class UriExtensions
    {
        /// <summary>
        /// Adds the specified parameter to the Query String.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramName">Name of the parameter to add.</param>
        /// <param name="paramValue">Value for the parameter to add.</param>
        /// <returns>Url with added parameter.</returns>
        public static Uri AddParameter(this Uri url, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue;
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the specified parameter to the Query String.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters">Name and value collection of the parameters to add</param>
        /// <returns>Url with added parameter.</returns>
        public static Uri AddParameters(this Uri url, params KeyPair<string, string>[] parameters)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var parameter in parameters)
            {
                query[parameter.Key] = parameter.Value;
            }
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the specified parameter to the Query String.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters">Name and value collection of the parameters to add</param>
        /// <returns>Url with added parameter.</returns>
        public static Uri AddParameters(this Uri url, NameValueCollection parameters)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (string key in parameters)
            {
                query[key] = parameters[key];
            }
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }
    }
}
