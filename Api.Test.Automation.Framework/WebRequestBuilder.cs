using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Http;
using Api.Test.Automation.Framework.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Test.Automation.Framework
{
    public class WebRequestBuilder
    {
        private const string DefaultContentType = "application/json;charset=utf-8";
        private const string CorrelationKey = "x-correlation";
        private const string AuthorizationHeaderKey = "Authorization";

        private readonly HttpWebRequest _webRequest;

        /// <summary>
        /// Allows the user to fluently build a web request object for testing purposes. Pass this web request to ApiRequestProcessor to test APIs
        /// </summary>
        /// <param name="url">Url of the Http endpoint</param>
        public WebRequestBuilder(string url)
        {
            Uri test = new Uri(url, dontEscape: true);
            _webRequest = WebRequest.CreateHttp(test) as HttpWebRequest;
        }

        public WebRequestBuilder WithRequestMethod(HttpMethod requestMethod)
        {
            _webRequest.Method = requestMethod.ToString();
            return this;
        }

        public WebRequestBuilder WithRequestData(object requestData)
        {
            var jsonString = JsonConvert.SerializeObject(requestData);

            using (var streamWriter = new StreamWriter(_webRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonString);
            }

            return this;
        }

        public WebRequestBuilder WithRequestData(string requestData)
        {
            using (var streamWriter = new StreamWriter(_webRequest.GetRequestStream()))
            {
                streamWriter.Write(requestData);
            }

            return this;
        }

        public WebRequestBuilder WithContentType(string contentType)
        {
            _webRequest.ContentType = contentType;
            return this;
        }

        public WebRequestBuilder WithReferer(string referer)
        {
            _webRequest.Referer = referer;
            return this;
        }

        public WebRequestBuilder WithHeaderValue(string key, string value, bool setIfNull = true)
        {
            if (value != null || setIfNull)
            {
                _webRequest.Headers[key] = value;
            }
            return this;
        }

        public WebRequestBuilder WithProxy(WebProxy proxy)
        {
            _webRequest.Proxy = proxy;
            return this;
        }

        public WebRequestBuilder WithOAuthToken(string oauthToken, bool setIfNull = true)
        {
            if (oauthToken != null || setIfNull)
            {
                _webRequest.Headers[AuthorizationHeaderKey] = $"Bearer {oauthToken}";
            }
            return this;
        }

        /// <summary>
        /// Adds correlation header *unless* the passed value is null (allows for easier use when chaining methods calls)
        /// </summary>
        /// <param name="correlation"></param>
        /// <returns></returns>
        public WebRequestBuilder WithCorrelation(Correlation correlation, bool setIfNull = true)
        {
            if (correlation != null || setIfNull)
            {
                _webRequest.Headers[CorrelationKey] = JsonConvert.SerializeObject(correlation, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
            return this;
        }

        public WebRequestBuilder WithCertificateAuthentication(CertificateAuthenticationCredentials credentials)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var networkCredential = new NetworkCredential(credentials.UserName, credentials.Password);
            _webRequest.Credentials = new CredentialCache
            {
                {
                    _webRequest.RequestUri,
                    credentials.Protocol.ToString(),
                    networkCredential
                }
            };

            return this;
        }

        public WebRequestBuilder WithCacheTime(int cacheTime)
        {
            _webRequest.CachePolicy = new HttpRequestCachePolicy(HttpCacheAgeControl.MaxAge, TimeSpan.FromHours(cacheTime));
            return this;
        }

        public HttpWebRequest Build()
        {
            if (string.IsNullOrEmpty(_webRequest.ContentType))
            {
                _webRequest.ContentType = DefaultContentType;
            }

            return _webRequest;
        }
    }
}
