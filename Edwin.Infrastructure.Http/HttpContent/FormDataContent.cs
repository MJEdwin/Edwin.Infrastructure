using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Edwin.Infrastructure.Http.HttpContent
{
    public class FormDataContent : StringContent
    {
        public FormDataContent(string key, string value) : base(value, Encoding.UTF8)
        {
            if (Headers.ContentDisposition == null)
                Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            Headers.ContentDisposition.Name = "\"" + key + "\"";
        }
    }
}
