using System;
using System.Net;
using Newtonsoft.Json;

namespace Api.Test.Automation.Framework.Model
{
    public class ApiResponse
    {
        /// <summary>
        /// The unparsed response body from an HttpWebRequest
        /// </summary>
        public string RawBody { get; set; }

        /// <summary>
        /// Status description from the HttpWebRequest
        /// </summary>
        public string StatusDescription { get; set; }

        /// <summary>
        /// Status Code from the HttpWebRequest
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Headers from the HttpWebRequest
        /// </summary>
        public WebHeaderCollection Headers { get; set; }

        /// <summary>
        /// The milliseconds taken for the response to be returned from the endpoint
        /// </summary>
        public double ResponseTime { get; set; }
        
        public WebException WebException { get; internal set; }

        [Obsolete("Use WebException.Status")]
        public WebExceptionStatus ExceptionStatus => WebException.Status;

        [Obsolete("Use WebException.Message")]
        public string ExceptionMessage => WebException.Message;
        
        /// <summary>
        /// Deserialises the json response body into the generic type T
        /// </summary>
        public T ParseResponse<T>()
        {
            return JsonConvert.DeserializeObject<T>(RawBody);
        }
    }
}
