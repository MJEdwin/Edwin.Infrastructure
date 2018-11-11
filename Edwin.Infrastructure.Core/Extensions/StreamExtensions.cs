using Edwin.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Edwin.Infrastructure.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ReadAsBytes(this Stream stream, long start = 0, long? end = null)
        {
            Guard.ArgumentNotNull(nameof(stream), stream);
            Guard.ArgumentValid(start < stream.Length, nameof(start), "start must less than stream length");
            Guard.OperationValid(stream.CanRead, "stream can not read");

            List<byte> readBytes = new List<byte>();
            stream.Position = start;
            long readLength = (end ?? stream.Length) - start;
            for (long i = 0; i < readLength; i++)
            {
                readBytes.Add(Convert.ToByte(stream.ReadByte()));
            }
            return readBytes.ToArray();
        }

        public static Stream ReadAsStream(this Stream stream, long start = 0, long? end = null)
        {
            return new MemoryStream(stream.ReadAsBytes(start, end));
        }

        public static Stream Insert(this Stream stream, Stream other)
        {
            Guard.ArgumentNotNull(nameof(stream), stream);
            Guard.ArgumentNotNull(nameof(other), other);

            other.Seek(0, SeekOrigin.Begin);
            int readByte;
            while ((readByte = other.ReadByte()) != -1)
            {
                stream.WriteByte(Convert.ToByte(readByte));
            }
            return stream;
        }

        public static Stream Append(this Stream stream, Stream other)
        {
            stream.Position = stream.Length;
            return Insert(stream, other);
        }

        public static Stream Prepend(this Stream stream, Stream other)
        {
            stream.Position = 0;
            return Insert(stream, other);
        }
    }
}
