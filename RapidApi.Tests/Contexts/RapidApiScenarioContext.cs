using Api.Test.Automation.Framework.Model;

namespace RapidApiTests.Contexts
{
    public class RapidApiScenarioContext
    {
        public ApiResponse GetRapidApiResponse { get; internal set; }
        public ApiResponse PostRapidApiResponse { get; internal set; }
        public ApiResponse DeleteRapidApiResponse { get; internal set; }
        public ApiResponse AuthResponse { get; internal set; }
    }
}
