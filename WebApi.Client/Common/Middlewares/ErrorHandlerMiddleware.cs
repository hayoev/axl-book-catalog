using System;
using System.Net;
using System.Threading.Tasks;
using Application.Client.Common.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApi.Client.Common.Responses;

namespace WebApi.Client.Common.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ErrorHandlerMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var env = context.RequestServices.GetRequiredService<IWebHostEnvironment>();

                var response = context.Response;
                response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                ErrorResponse errorResponse;

                switch (error)
                {
                    case ValidationException e:
                        errorResponse = new ErrorResponse(ErrorStatusCodeEnum.BAD_REQUEST, e.Message, e.Errors);
                        break;
                    case FluentValidation.ValidationException e:
                        errorResponse = new ErrorResponse(ErrorStatusCodeEnum.BAD_REQUEST, e.Message);
                        break;
                    case LogicException e:
                        errorResponse = new ErrorResponse(ErrorStatusCodeEnum.BAD_REQUEST, e.Message);
                        break;
                    
                    case UnauthorizedException _:
                        errorResponse = new ErrorResponse(ErrorStatusCodeEnum.ERROR_AUTH, "Ошибка авторизации");
                        break;
                    case TokenExpiredException _:
                        errorResponse = new ErrorResponse(ErrorStatusCodeEnum.TOKEN_EXPIRED, "Срок токена истек");
                        break;
                    case RefreshTokenExpiredException _:
                        errorResponse = new ErrorResponse(ErrorStatusCodeEnum.TOKEN_REFRESH_EXPIRED,
                            "Срок токена истек");
                        break;
                    default:
                        if (env.IsDevelopment())
                        {
                            throw;
                        }

                        errorResponse = new ErrorResponse(ErrorStatusCodeEnum.UNHANDLED_EXCEPTION,
                            "Неизвестная ошибка!");

                        _logger.LogError(
                            "Request {method} {url} => Message: {message}, StackTrace: {stackTrace}",
                            context.Request?.Method,
                            context.Request?.Path.Value,
                            error.Message, error.StackTrace);

                        break;
                }

                var contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };


                await response.WriteAsync(JsonConvert.SerializeObject(errorResponse, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                }));
            }
        }
    }
}