using Common.Config;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Common.Middleware;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;

    public TokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestPath = context.Request.Path.Value;
        var httpMethod = context.Request.Method;

        // Eğer endpoint bypass listesinde ise doğrudan devam et
        if (TokenValidationConfig.BypassEndpoints.Contains(requestPath))
        {
            await _next(context);
            return;
        }

        // Endpoint ve method'a göre gerekli role'ü bul
        var requiredRole = GetRequiredRoleForEndpoint(requestPath, httpMethod);

        // Token'ı al
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Token gerekli ancak sağlanmadı.");
            return;
        }

        // Token doğrulama isteğini yap
        var validateResponse = await ValidateTokenAsync(token, requiredRole);
        if (!validateResponse.IsSuccessStatusCode)
        {
            context.Response.StatusCode = (int)validateResponse.StatusCode;
            await context.Response.WriteAsync(await validateResponse.Content.ReadAsStringAsync());
            return;
        }

        await _next(context);
    }

    private async Task<HttpResponseMessage> ValidateTokenAsync(string token, string requiredRole)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.PostAsJsonAsync("https://localhost:7032/api/auth/validate-token", new
        {
            Token = token,
            RequiredRole = requiredRole
        });
        return response;
    }

    // Endpoint'e ve HTTP method'a göre gerekli role'ü bul
    private string GetRequiredRoleForEndpoint(string requestPath, string httpMethod)
    {
        var endpointMapping = TokenValidationConfig.EndpointMappings
            .FirstOrDefault(e => e.Endpoint == requestPath && e.HttpMethod == httpMethod);

        // Eğer eşleşen bir mapping varsa, required permission'ı döndür
        return endpointMapping?.RequiredPermission ?? string.Empty;
    }
}
