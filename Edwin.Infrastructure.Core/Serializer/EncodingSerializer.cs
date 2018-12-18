using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Serializer
{
    public class EncodingSerializer : ISerializer<string, byte[]>
    {
        private readonly Encoding _encoding;

        public EncodingSerializer(Encoding encoding)
        {
            _encoding = encoding;
        }

        public EncodingSerializer(string codeName)
        {
            _encoding = Encoding.GetEncoding(codeName);
        }

        public EncodingSerializer(int codePage)
        {
            _encoding = Encoding.GetEncoding(codePage);
        }

        public string Deserialize(byte[] destination)
            => _encoding.GetString(destination);

        public byte[] Serialize(string sourse)
            => _encoding.GetBytes(sourse);
    }
}
