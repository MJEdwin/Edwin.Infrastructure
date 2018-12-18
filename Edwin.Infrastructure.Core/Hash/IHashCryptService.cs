using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Hash
{
    public interface IHashCryptService
    {
        byte[] ComputeHash(byte[] data);

        bool CompareHash(byte[] data, byte[] hash);
    }
}
