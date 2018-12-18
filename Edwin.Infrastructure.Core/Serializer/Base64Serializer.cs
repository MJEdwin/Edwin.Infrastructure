using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Serializer
{
    public class Base64Serializer : ISerializer<string, byte[]>
    {
        public string Deserialize(byte[] destination)
            => Convert.ToBase64String(destination);

        public byte[] Serialize(string sourse)
            => Convert.FromBase64String(sourse);
    }
}
