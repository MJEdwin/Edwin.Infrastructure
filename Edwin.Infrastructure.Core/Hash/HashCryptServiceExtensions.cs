using Edwin.Infrastructure.Serializer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Hash
{
    public static class HashCryptServiceExtensions
    {
        public static byte[] ComputeHash(this IHashCryptService service, string data, ISerializer<string, byte[]> dataSerializer, ISerializer<byte[], string> hashSerializer) 
            => service.ComputeHash(dataSerializer.Serialize(data));
    }
}
