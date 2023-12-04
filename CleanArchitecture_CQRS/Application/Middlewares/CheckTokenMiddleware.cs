
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace Application.Middlewares
{
    public class CheckTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();

                try
                {
                    var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                    if (jsonToken != null && jsonToken.ValidTo < DateTime.UtcNow)
                    {
                        httpContext.Response.StatusCode = 401;

                        await WriteJsonResponse(httpContext, "Token has expired. Please log in again.");
                        return;
                    }
                }
                catch (SecurityTokenExpiredException)
                {
                    httpContext.Response.StatusCode = 401;
                    await WriteJsonResponse(httpContext, "Token read faile!");
                    return;
                }
            }

            await _next(httpContext);
        }

        private async Task WriteJsonResponse(HttpContext httpContext, string message)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new { message };
            var jsonResponse = JsonSerializer.Serialize(response);
            await httpContext.Response.WriteAsync(jsonResponse);
        }
    }
}
