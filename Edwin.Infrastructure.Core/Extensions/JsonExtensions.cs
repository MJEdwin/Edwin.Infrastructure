using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJsonString(this object @object)
        {
            return JsonConvert.SerializeObject(@object);
        }

        public static T ToObject<T>(this string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static JToken ToJToken(this object @object)
        {
            return JToken.FromObject(@object);
        }

        public static JObject ToJObject(this object @object)
        {
            return JObject.FromObject(@object);
        }

        public static JArray ToJArray(this IEnumerable<object> @array)
        {
            return JArray.FromObject(@array);
        }

        public static JToken ToJToken(this string jsonString)
        {
            return JToken.Parse(jsonString);
        }
    }
}
