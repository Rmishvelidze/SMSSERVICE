using System.Net;

namespace SmsService.Midlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new
            {
                httpContext.Response.StatusCode,
                Message = "internal Server Error from the custom middleware",
                Path = "path-goes-here",
                exception = ex.Message
            };

            return httpContext.Response.WriteAsync(response.ToString());
        }
    }
}
