using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Blog.App.Middlewares
{
    public class ErrorHandleMiddlewares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandleMiddlewares> _logger;

        public ErrorHandleMiddlewares(
            RequestDelegate next,
            ILogger<ErrorHandleMiddlewares> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured");

                var error = new { message = ex.Message };
                var errorJson = JsonConvert.SerializeObject(error);
                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(errorJson, Encoding.UTF8);
            }
        }
    }
}
