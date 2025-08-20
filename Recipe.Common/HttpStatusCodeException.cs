﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Common
{
    public class HttpStatusCodeException:Exception
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; } = @"text/plain";
        public string Code { get; set; }
        public JToken Data { get; set; } = new JArray(); 

        public HttpStatusCodeException(int statusCode)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(int statusCode, string message, string code = "0")
            : base(message)
        {
            this.ContentType = @"application/json";
            this.StatusCode = statusCode;
            this.Code = code;
        }

        public HttpStatusCodeException(int statusCode, string message, JToken data, string code = "0")
            : base(message)
        {
            this.ContentType = @"application/json";
            this.StatusCode = statusCode;
            this.Code = code;
            this.Data = data;
        }

        public HttpStatusCodeException(int statusCode, Exception inner, string code = "0") : this(statusCode, inner.ToString(), code) { }

        public HttpStatusCodeException(int statusCode, JObject errorObject, string code = "0")
            : base(errorObject.ToString())
        {
            this.ContentType = @"application/json";
            this.StatusCode = statusCode;
            this.Code = code;
            this.Data = errorObject;
        }
    
}
}
