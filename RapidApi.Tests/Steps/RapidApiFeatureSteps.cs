using System;
using System.Net.Http;
using Api.Test.Automation.Framework;
using Api.Test.Automation.Framework.Model;
using FluentAssertions;
using RapidApiTests.Contexts;
using RapidApiTests.Model;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RapidApiTests.Steps
{
    [Binding]
    public class RapidApiFeatureSteps
    {
        private const string ApiRootUrl = "http://localhost:3000";
        private readonly RapidApiScenarioContext _scenarioContext;

        public RapidApiFeatureSteps(RapidApiScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have created a new blog post:")]
        public void GivenIHaveCreatedANewPost(Table table)
        {
            CreateNewBlogPost(table);
        }

        [Given(@"I have authenticated with the application")]
        public void GivenIHaveAuthenticatedWithTheApplication()
        {
            var authEndpoint = "AUTH_ENDPOINT";
            var clientId = "CLIENTID";
            var clientSecret = "CLIENT_SECRET";

            var url = $"{authEndpoint}?clientId={clientId}&clientSecret={clientSecret}";

            var webRequest = new WebRequestBuilder(url)
                .Build();

            _scenarioContext.AuthResponse = ApiRequestProcessor.Call(webRequest);
        }

        [When(@"I get the created blog post")]
        public void WhenIGetTheCreatedPost()
        {
            var createdPost = _scenarioContext.PostRapidApiResponse.ParseResponse<BlogPost>();
            var getRequest = new WebRequestBuilder($"{ApiRootUrl}/blogPosts/{createdPost.id}")
                .WithRequestMethod(HttpMethod.Get)
                .Build();

            _scenarioContext.GetRapidApiResponse = ApiRequestProcessor.Call(getRequest);
        }

        [When(@"I create a new blog post:")]
        public void WhenICreateANewPost(Table table)
        {
            CreateNewBlogPost(table);
        }

        [Then(@"the result should be:")]
        public void ThenTheResultShouldBe(Table table)
        {
            var getBlogPostResponse = _scenarioContext.GetRapidApiResponse.ParseResponse<BlogPost>();
            var expectedBlogPost = table.CreateInstance<BlogPost>();

            getBlogPostResponse.Title.Should().Be(expectedBlogPost.Title);
            getBlogPostResponse.Author.Should().Be(expectedBlogPost.Author);
        }

        [Then(@"the '(.*)' response should be received in (.*) milliseconds")]
        public void ThenTheResponseShouldBeReceivedInMilliseconds(string responseType, int maxMilliseconds)
        {
            var apiResponse = GetResponseFromContext(responseType);

            apiResponse.ResponseTime.Should().BeLessThan(maxMilliseconds);
        }

        [Then(@"the '(.*)' response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(string responseType, int expectedStatusCode)
        {
            var apiResponse = GetResponseFromContext(responseType);

            apiResponse.StatusCode.Should().Be(expectedStatusCode);
        }

        private void CreateNewBlogPost(Table table)
        {
            var blogPost = table.CreateInstance<BlogPost>();

            var createBlogPostRequest = new WebRequestBuilder($"{ApiRootUrl}/blogPosts")
                .WithRequestMethod(HttpMethod.Post)
                .WithRequestData(blogPost)
                .Build();

            _scenarioContext.PostRapidApiResponse = ApiRequestProcessor.Call(createBlogPostRequest);
        }

        private ApiResponse GetResponseFromContext(string responseType)
        {
            switch (responseType)
            {
                case "Get":
                    return _scenarioContext.GetRapidApiResponse;
                case "Delete":
                    return _scenarioContext.DeleteRapidApiResponse;
                case "Post":
                    return _scenarioContext.PostRapidApiResponse;
                default:
                    throw new ArgumentException("Unsupported response type", responseType);
            }
        }
    }
}
