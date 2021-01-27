using Api.Test.Automation.Framework.Model;

namespace RapidApiTests.Contexts
{
    public class TemplateScenarioContext
    {
        public ApiResponse CreateBlogPostResponse { get; internal set; }
        public ApiResponse GetBlogPostResponse { get; internal set; }
        public ApiResponse DeleteBlogPostResponse { get; internal set; }
        public ApiResponse AuthResponse { get; internal set; }
    }
}
