using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Hash
{
    public interface ICryptService
    {
        string HashToString(byte[] data);

        string HashToString(string data, Encoding encoding);

        byte[] Hash(byte[] data);

        byte[] Hash(string data, Encoding encoding);
    }
}
