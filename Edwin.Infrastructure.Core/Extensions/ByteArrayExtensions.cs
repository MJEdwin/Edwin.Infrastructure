using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Extensions
{
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Convert Byte Array To String
        /// </summary>
        /// <param name="data">Byte Array</param>
        /// <param name="format">Format</param>
        /// <returns>String</returns>
        public static string ToString(this byte[] data, string format)
        {
            string convert = string.Empty;
            foreach (var item in data)
            {
                convert += item.ToString(format);
            }
            return convert;
        }

        public static string ToString(this byte[] data,IFormatProvider provider)
        {
            string convert = string.Empty;
            foreach (var item in data)
            {
                convert += item.ToString(provider);
            }
            return convert;
        }
    }
}
