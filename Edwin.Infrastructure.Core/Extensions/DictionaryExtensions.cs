using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Edwin.Infrastructure.Extensions
{
    public static class DictionaryExtensions
    {
        public static string EncodeAsParam(this Dictionary<string, string> dict, bool isEncoded)
        {
            List<string> paramList = new List<string>();
            foreach (var item in dict)
            {
                paramList.Add(item.Key + "=" + (isEncoded ? WebUtility.UrlEncode(item.Value) : item.Value));
            }
            return string.Join("&", paramList);
        }
    }
}
