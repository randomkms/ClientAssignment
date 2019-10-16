using System;
using Newtonsoft.Json;

namespace ClientAssignment.Models
{
    public class ApiException : Exception
    {
        [JsonProperty("result")]
        public bool Result { get; }

        [JsonProperty("error")]
        public override string Message { get; }

        public ApiException(bool result, string error)
        {
            this.Result = result;
            this.Message = error;
        }
    }
}
