using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using System.Net;

namespace SmsService.Midlewares
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureBuildInExceptionHanlder(this IApplicationBuilder application, ILoggerFactory loggerFactory)
        {
            application.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var logger = loggerFactory.CreateLogger("ConfigureBuildInExceptionHanlder");

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var contextRequest = context.Features.Get<IHttpRequestFeature>();

                    if (contextFeature != null)
                    {
                        var errorMessage = new
                        {
                            context.Response.StatusCode,
                            contextFeature.Error.Message,
                            contextRequest.Path
                        }.ToString();

                        await context.Response.WriteAsync(errorMessage);
                    }
                });
            });
        }

        public static void ConfigureCustomExceptionHandler(this IApplicationBuilder application)
        {
            application.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
