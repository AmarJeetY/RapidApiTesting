using System.IO;
using System.Net;
using Api.Test.Automation.Framework.Model;

namespace Api.Test.Automation.Framework
{
    internal static class ApiResponseParser
    {
        internal static ApiResponse GetResponseData(this HttpWebResponse response, double responseTime)
        {
            var parsedResponse = new ApiResponse
            {
                StatusDescription = response.StatusDescription,
                StatusCode = (int)response.StatusCode,
                Headers = response.Headers,
                ResponseTime = responseTime
            };

            using (var responseStream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(responseStream))
                {
                    parsedResponse.RawBody = reader.ReadToEnd();
                }
            }

            return parsedResponse;
        }

        internal static ApiResponse GetResponseData(this WebException webException, double responseTime)
        {
            var response = new ApiResponse
            {
                ResponseTime = responseTime,
                WebException = webException,
            };

            if (webException.Status == WebExceptionStatus.ProtocolError)
            {
                if (webException.Response is HttpWebResponse webResponse)
                {
                    response.StatusCode = (int)webResponse.StatusCode;
                    response.StatusDescription = webResponse.StatusDescription;
                    response.Headers = response.Headers;
                }
            }

            if (webException.Response != null)
            {
                using (var responseStream = webException.Response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        response.RawBody = reader.ReadToEnd();
                    }
                }
            }

            return response;
        }
    }
}