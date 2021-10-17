using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TopLogicApp.API.Middlewares
{
    public class TestMiddleware
    {
        readonly RequestDelegate _next;

        public TestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await _next(httpContext);
        }
    }
}
