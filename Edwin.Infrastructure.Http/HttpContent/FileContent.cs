using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Edwin.Infrastructure.Http.HttpContent
{
    public class FileContent : StreamContent
    {
        public FileContent(string filePath, string mediaType = null) : this(File.OpenRead(filePath), Path.GetFileName(filePath), mediaType)
        {
        }

        public FileContent(Stream fileStream, string fileName = null, string mediaType = null) : base(fileStream)
        {
            FileName = fileName;
            Length = fileStream.Length;
            ContentType = !string.IsNullOrEmpty(mediaType) ? mediaType : "application/octet-stream";
            Headers.ContentType = new MediaTypeHeaderValue(ContentType);
            Headers.ContentDisposition = Headers.ContentDisposition ?? new ContentDispositionHeaderValue("attachment");
            Headers.ContentDisposition.FileName = "\"" + FileName + "\"";
            Headers.ContentLength = Length;
        }

        public string FileName { get; private set; }

        public long Length { get; private set; }

        public string ContentType { get; private set; }
    }
}
