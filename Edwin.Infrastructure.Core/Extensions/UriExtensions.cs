using Edwin.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Edwin.Infrastructure.Extensions
{
    public static class UriExtensions
    {
        public static Dictionary<string, string> GetQueryParam(this Uri uri)
        {
            Guard.ArgumentNotNullOrEmpty(uri.Query, "uri query is empty");

            var paramList = uri.Query.TrimStart('?').Split('&');
            Dictionary<string, string> param = new Dictionary<string, string>();
            foreach (var item in paramList)
            {
                var keyValuePair = item.Split('=');
                param.Add(keyValuePair[0], HttpUtility.UrlDecode(keyValuePair[1]));
            }
            return param;
        }
    }
}
