using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Api.Test.Automation.Framework;
using Api.Test.Automation.Framework.Model;
using RapidApiTests.Model;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace RapidApiTests.Steps
{
    [Binding]
    class VehicleDetailsSteps
    {
        private const string Uri = @"https://covea-public-mock-x-api-direct-sitb.uk-e1.cloudhub.io/api/v1/vehicledetails";
        private readonly IDictionary<string, ApiResponse> _vehicleDetailsResponse = new Dictionary<string, ApiResponse>();
        private IEnumerable<RequestParameters> _requestParameters = null;

        [When(@"I GET vehicle details")]
        public void GetVehicleDetails(Table table)
        {
            _requestParameters = table.CreateSet<RequestParameters>();
            foreach (var param in _requestParameters)
            {
                ApiResponse response = null;
                HttpMethod method = null;
                switch (param.Method)
                {
                    case "Get":
                        method = HttpMethod.Get; break;
                    case "DELETE":
                        method = HttpMethod.Delete; break;
                    default:
                        method = HttpMethod.Get; break;
                }

                var createGetVehicleDetailsRequest = new WebRequestBuilder(Uri + "/vehicle/" + param.Parameter)
                    .WithRequestMethod(method)
                    .WithHeaderValue("Authorization", "Basic")
                    .WithHeaderValue("ClientID", param.ClientId)
                    .WithHeaderValue("ClientSecret", param.ClientSecret)
                    .WithHeaderValue("ContentType", param.ContentType)
                    .Build();
                response = ApiRequestProcessor.Call(createGetVehicleDetailsRequest);

                _vehicleDetailsResponse.Add(param.TestName, response);
            }
        }
        [Then(@"verify response time")]
        public void ThenVerifyResponseTime(Table table)
        {
            var validateResponse = table.CreateSet<ValidateResponse>();

            foreach (var test in _vehicleDetailsResponse.Keys)
            {
                var response = (ValidateResponse)(from row in validateResponse
                                                where row.TestName == test
                                                select row).First();
                Assert.True(_vehicleDetailsResponse[test].ResponseTime < Double.Parse(response.MaxResponseTime),
                    "Expected : " + response.MaxResponseTime + " Actual : " + _vehicleDetailsResponse[test].ResponseTime);
            }
        }

        [Then(@"verify response code")]
        public void ThenVerifyResponseCode(Table table)
        {
            var validateResponse = table.CreateSet<ValidateResponse>();

            foreach (var test in _vehicleDetailsResponse.Keys)
            {
                var response = (ValidateResponse)(from row in validateResponse
                                                where row.TestName == test
                                                select row).First();
                Assert.True(_vehicleDetailsResponse[test].StatusCode.Equals(Int16.Parse(response.StatusCode)),
                    "Expected : " + response.StatusCode + " Actual : " + _vehicleDetailsResponse[test].StatusCode);
            }
        }
    }
}
