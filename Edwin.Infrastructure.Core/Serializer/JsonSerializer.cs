using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Serializer
{
    public class JsonSerializer : ISerializer<object, string>
    {
        private readonly JsonSerializerSettings _serializerSettings = null;
        private readonly Type _srcType;

        public JsonSerializer(Type srcType)
        {
            _srcType = srcType;
        }

        public JsonSerializer(Type srcType, JsonSerializerSettings serializerSettings) : this(srcType)
        {
            _serializerSettings = serializerSettings;
        }

        public object Deserialize(string destination)
        {
            return _serializerSettings == null ? JsonConvert.DeserializeObject(destination, _srcType) : JsonConvert.DeserializeObject(destination, _srcType, _serializerSettings);
        }

        public string Serialize(object sourse)
        {
            return _serializerSettings == null ? JsonConvert.SerializeObject(sourse) : JsonConvert.SerializeObject(sourse, _serializerSettings);
        }
    }

    public class JsonSerializer<T> : JsonSerializer, ISerializer<T, string>
    {
        public JsonSerializer() : base(typeof(T)) { }
    
        public JsonSerializer(JsonSerializerSettings serializerSettings) : base(typeof(T), serializerSettings) { }

        public new T Deserialize(string destination)
            => (T)base.Deserialize(destination);

        public string Serialize(T sourse)
            => base.Serialize(sourse);
    }
}
