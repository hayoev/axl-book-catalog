using Microsoft.AspNetCore.Builder;
using WebApi.Admin.Common.Middlewares;

namespace WebApi.Admin.Common.Extensions
{
    public static class ErrorHandlerExtension
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}