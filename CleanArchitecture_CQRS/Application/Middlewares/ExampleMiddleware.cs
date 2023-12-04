
using Microsoft.AspNetCore.Http;

namespace Application.Middlewares
{
    public class ExampleMiddleware
    {
        private readonly RequestDelegate _next;
        public ExampleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("Request");
            await _next(context);
            Console.WriteLine("Response");
        }
    }
}
