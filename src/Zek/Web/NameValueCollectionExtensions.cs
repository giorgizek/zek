using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Zek.Web
{
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nvc"></param>
        /// <returns></returns>
        public static string ToQueryString(this NameValueCollection nvc)
        {
            return string.Join("&",
                nvc.AllKeys.Select(
                    key => string.Format("{0}={1}",
                        HttpUtility.UrlEncode(key),
                        HttpUtility.UrlEncode(nvc[key]))));
        }
    }
}
