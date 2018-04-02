using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edwin.Infrastructure.DDD.Domain
{
    public static class EntityExtension
    {
        public static void ChangeProperty<TPrimaryKey>(this IEntity<TPrimaryKey> entity, string key, object value)
            where TPrimaryKey : IEquatable<TPrimaryKey>
        {
            var props = entity.GetType().GetProperties();
            var prop = props.FirstOrDefault(p => p.Name == key);
            if (prop != null)
                prop.SetValue(entity, value);
        }

        public static void ChangeProperty<TPrimaryKey>(this IEntity<TPrimaryKey> entity, Dictionary<string, object> dictionary)
            where TPrimaryKey : IEquatable<TPrimaryKey>
        {
            foreach (var prop in entity.GetType().GetProperties())
            {
                if (dictionary.ContainsKey(prop.Name))
                {
                    prop.SetValue(entity, dictionary[prop.Name]);
                }
            }
        }

        public static void ChangeProperty<TPrimaryKey>(this IEntity<TPrimaryKey> entity, JObject @object)
            where TPrimaryKey : IEquatable<TPrimaryKey>
        {
            foreach (var prop in entity.GetType().GetProperties())
            {
                if (@object.ContainsKey(prop.Name))
                {
                    prop.SetValue(entity, @object[prop.Name].ToObject(prop.PropertyType));
                }
            }
        }
    }
}
