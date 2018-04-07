using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenter.API.Response
{
    public class ResponseSheme
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public int DataCount { get; set; }
        public object Data { get; set; }

        public ResponseSheme(object data , string message, int statusCode, int dataCount=0)
        {
            this.Data = data;
            this.DataCount = dataCount;
            this.Message = message;
            this.StatusCode = statusCode;
        }
    }
}