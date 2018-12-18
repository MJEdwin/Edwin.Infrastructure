using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Edwin.Infrastructure.Extensions
{
    public static class DictionaryExtensions
    {
        public static string EncodeAsQueryParam(this IDictionary<string, string> dict, bool isEncoded = false)
        {
            List<string> paramList = new List<string>();
            foreach (var item in dict)
            {
                if (item.Value != null)
                {
                    paramList.Add(item.Key + "=" + (isEncoded ? WebUtility.UrlEncode(item.Value) : item.Value));
                }
            }
            return string.Join("&", paramList);
        }
    }
}
