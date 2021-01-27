namespace Api.Test.Automation.Framework.Model
{
    /// <summary>
    /// Response from an Authentication API which returns an OAuth token
    /// </summary>
    public class AuthApiResponse
    {
        /// <summary>
        /// OAuth token to attach to authenticated API calls
        /// </summary>
        public string access_token { get; set; }
    }
}
