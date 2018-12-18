using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object @object) 
            => JsonConvert.SerializeObject(@object);

        public static T ToObject<T>(this string jsonStr) 
            => JsonConvert.DeserializeObject<T>(jsonStr);

        public static JToken AsJToken(this object @object) 
            => JToken.FromObject(@object);

        public static JToken ToJToken(this string jsonStr) 
            => JToken.Parse(jsonStr);
    }
}
