using System;
using System.Net;

namespace BackendCommon
{
    public class RestException : Exception
    {
        public RestException(HttpStatusCode statusCode, string message) : base()
        {
            this.StatusCode = statusCode;
            this.Message = message;
        }

        public HttpStatusCode StatusCode { get; }

        public override string Message { get; }
    }
}
