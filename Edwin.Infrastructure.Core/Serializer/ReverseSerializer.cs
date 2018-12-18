using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Serializer
{
    public class ReverseSerializer<TSerializeFrom, TSerializeTo> : ISerializer<TSerializeFrom, TSerializeTo>
    {
        private readonly ISerializer<TSerializeTo, TSerializeFrom> _serializer;

        public ReverseSerializer(ISerializer<TSerializeTo, TSerializeFrom> serializer)
        {
            _serializer = serializer;
        }

        public TSerializeFrom Deserialize(TSerializeTo destination)
            => _serializer.Serialize(destination);

        public TSerializeTo Serialize(TSerializeFrom sourse)
            => _serializer.Deserialize(sourse);
    }
}
