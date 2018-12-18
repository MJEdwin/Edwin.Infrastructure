﻿using Edwin.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Edwin.Infrastructure.Http.HttpContent
{
    public class JsonContent : StringContent
    {
        public JsonContent(string jsonStr, Encoding encoding = null) : base(jsonStr, encoding ?? Encoding.UTF8, "application/json")
        {
        }

        public JsonContent(object @object, Encoding encoding = null) : this(@object.ToJson(), encoding)
        {

        }
    }
}
