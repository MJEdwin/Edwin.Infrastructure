using Edwin.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Http.UriExtensions
{
    public class UriString
    {
        public Uri Uri { get; }

        public Dictionary<string, string> ParamDict { get; }

        public bool IsParamEncoded { get; set; }

        public UriString(string url, IDictionary<string, string> param = null, bool isParamEncoded = true) : this(new Uri(url), param, isParamEncoded) { }

        public UriString(string url, object param = null, bool isParamEncoded = true) : this(url, param?.ToDictionary(), isParamEncoded) { }

        public UriString(Uri uri, IDictionary<string, string> param = null, bool isParamEncoded = true)
        {
            Uri = uri;
            if (param != null)
            {
                ParamDict = new Dictionary<string, string>(param);
            }
            else
            {
                ParamDict = new Dictionary<string, string>();
            }
        }

        public void AddParam(string name, string value)
            => ParamDict.Add(name, value);

        public void DeleteParam(string name)
            => ParamDict.Remove(name);
        /// <summary>
        /// get uri with param
        /// </summary>
        /// <returns></returns>
        public Uri ToUri()
            => new Uri(ToString());

        /// <summary>
        /// get url string with param
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => Uri.ToString() + "?" + ParamDict.EncodeAsParam(IsParamEncoded);

        public static implicit operator UriString(Uri @uri) => new UriString(uri.AbsolutePath, uri.GetQueryParam());

    }
}
