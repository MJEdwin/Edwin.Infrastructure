using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Serializer
{
    public class DictionarySerializer : ISerializer<Dictionary<string, object>>
    {
        private readonly DictionarySerializeSetting _setting;
        public DictionarySerializer(DictionarySerializeSetting setting)
        {
            _setting = setting;
        }

        public Dictionary<string, object> Serialize(object sourse)
        {
            var dict = new Dictionary<string, object>();
            foreach (var prop in sourse.GetType().GetProperties())
            {
                dict.Add(_setting.ConvertName(prop.Name), prop.GetValue(sourse));
            }
            return dict;
        }

        public object Deserialize(Dictionary<string, object> destination, Type sourseType)
        {
            var entity = Activator.CreateInstance(sourseType);
            foreach (var prop in sourseType.GetProperties())
            {
                //匹配
                var key = _setting.ConvertName(prop.Name);
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
