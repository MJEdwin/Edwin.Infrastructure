using Edwin.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Edwin.Infrastructure.Hash
{
    public class MD5CryptService : ICryptService
    {
        private static MD5 md5 = new MD5CryptoServiceProvider();

        #region Singleton
        public static ICryptService Instance { get; } = new MD5CryptService();
        #endregion

        public byte[] Hash(byte[] info) => md5.ComputeHash(info);

        public byte[] Hash(string data, Encoding encoding)
            => Hash(encoding.GetBytes(data));

        public string HashToString(byte[] data)
            => Hash(data).ToString("X2");

        public string HashToString(string data, Encoding encoding)
            => Hash(data, encoding).ToString("X2");
    }
}
