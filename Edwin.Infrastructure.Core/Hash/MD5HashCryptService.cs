using Edwin.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace Edwin.Infrastructure.Hash
{
    public class MD5HashCryptService : IHashCryptService, IDisposable
    {
        private readonly MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        public bool CompareHash(byte[] data, byte[] hash)
            => md5.ComputeHash(data).SequenceEqual(hash);

        public byte[] ComputeHash(byte[] data)
            => md5.ComputeHash(data);

        public void Dispose()
        {
            md5.Dispose();
        }
    }
}
