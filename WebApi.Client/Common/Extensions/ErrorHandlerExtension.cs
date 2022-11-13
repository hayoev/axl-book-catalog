using Microsoft.AspNetCore.Builder;
using WebApi.Client.Common.Middlewares;

namespace WebApi.Client.Common.Extensions
{
    public static class ErrorHandlerExtension
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}