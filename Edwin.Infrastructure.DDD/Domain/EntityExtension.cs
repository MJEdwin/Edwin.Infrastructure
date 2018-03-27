using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edwin.Infrastructure.DDD.Domian
{
    public static class EntityExtension
    {
        public static void Update<TPrimaryKey>(this IEntity<TPrimaryKey> entity, string key, object value)
            where TPrimaryKey : IEquatable<TPrimaryKey>
        {
            var props = entity.GetType().GetProperties();
            var prop = props.FirstOrDefault(p => p.Name.ToLower() == key);
            if (prop != null)
                prop.SetValue(entity, value);
        }

        public static void Update<TPrimaryKey>(this IEntity<TPrimaryKey> entity, Dictionary<string, object> dictionary)
            where TPrimaryKey : IEquatable<TPrimaryKey>
        {
            foreach (var prop in entity.GetType().GetProperties())
            {
                var key = prop.Name.ToLower();
                if (dictionary.ContainsKey(key))
                {
                    prop.SetValue(entity, dictionary[key]);
                }
            }
        }

        public static void Update<TPrimaryKey>(this IEntity<TPrimaryKey> entity, JObject @object)
            where TPrimaryKey : IEquatable<TPrimaryKey>
        {
            foreach (var prop in entity.GetType().GetProperties())
            {
                var key = prop.Name.ToLower();
                if (@object.ContainsKey(key))
                {
                    prop.SetValue(entity, @object[key].ToObject(prop.PropertyType));
                }
            }
        }
    }
}
