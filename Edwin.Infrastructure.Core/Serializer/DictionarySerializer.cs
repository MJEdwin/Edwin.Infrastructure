using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Serializer
{
    public class DictionarySerializer<TData> : ISerializer<TData, Dictionary<string, object>>
    {
        private CompareWay compareWay;
        public DictionarySerializer(CompareWay way)
        {
            compareWay = way;
        }

        public string ConvertKey(string key)
        {
            switch (compareWay)
            {
                case CompareWay.Default:
                default:
                    return key;
                case CompareWay.ToLower:
                    return key.ToLower();
                case CompareWay.ToUpper:
                    return key.ToUpper();
            }
        }

        public Dictionary<string, object> Serialize(TData sourse)
        {
            var dict = new Dictionary<string, object>();
            foreach (var prop in typeof(TData).GetProperties())
            {
                dict.Add(ConvertKey(prop.Name), prop.GetValue(sourse));
            }
            return dict;
        }

        public TData Deserialize(Dictionary<string, object> destination)
        {
            var entity = Activator.CreateInstance<TData>();
            foreach (var prop in typeof(TData).GetProperties())
            {
                //匹配
                var key = ConvertKey(prop.Name);
                if (destination.ContainsKey(key))
                {
                    prop.SetValue(entity, destination[key]);
                }
            }

            return entity;
        }
    }

    public enum CompareWay
    {
        Default = 1,
        ToLower = 2,
        ToUpper = 4,
    }
}
