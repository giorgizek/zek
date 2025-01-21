using System.Collections.Specialized;
using System.Web;

namespace Zek.Web
{
    public static class QueryStringHelper
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
                    key => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(nvc[key])}"));
        }

        public static string ToQueryString(IDictionary<string, string> dictionary)
        {
            return string.Join("&",
                dictionary.Select(item => $"{HttpUtility.UrlEncode(item.Key)}={HttpUtility.UrlEncode(item.Value)}"));
        }
    }
}
