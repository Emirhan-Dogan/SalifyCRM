using System.Collections.Generic;

namespace Common.Config
{
    public static class TokenValidationConfig
    {
        public static readonly List<string> BypassEndpoints = new()
        {
            "/api/auth/validate-token",
            "/api/Auth/personal-login",
            "/api/Auth/personal-refresh-token",
            "/api/Auth/register"
        };

        public static readonly List<EndpointMapping> EndpointMappings = new()
        {
            new EndpointMapping("/api/Groups", "GET", "GetGroupsQuery"),
            new EndpointMapping("/api/Users", "POST", "CreateUserCommand")
        };
    }

    public class EndpointMapping
    {
        public string Endpoint { get; set; }
        public string HttpMethod { get; set; }
        public string RequiredPermission { get; set; }

        public EndpointMapping(string endpoint, string httpMethod, string requiredPermission)
        {
            Endpoint = endpoint;
            HttpMethod = httpMethod;
            RequiredPermission = requiredPermission;
        }
    }
}
