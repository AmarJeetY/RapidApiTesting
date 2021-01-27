using System.Diagnostics;
using System.Net;
using Api.Test.Automation.Framework.Model;

namespace Api.Test.Automation.Framework
{
    public static class ApiRequestProcessor
    {
        /// <summary>
        /// Performs a web request and returns a standard API response, along with the time executed for the Http call
        /// </summary>
        /// <param name="webRequest">Web request to execute</param>
        /// <returns>ApiResponse</returns>
        public static ApiResponse Call(HttpWebRequest webRequest)
        {
            Stopwatch requestStopwatch = Stopwatch.StartNew();
            double responseTime;
            HttpWebResponse response;
            try
            {
                response = webRequest.GetResponse() as HttpWebResponse;
                requestStopwatch.Stop();
                responseTime = requestStopwatch.Elapsed.TotalMilliseconds;
                return response.GetResponseData(responseTime);
            }
            catch (WebException responseException)
            {
                requestStopwatch.Stop();
                responseTime = requestStopwatch.Elapsed.TotalMilliseconds;
                return responseException.GetResponseData(responseTime);
            }
        }
    }
}
