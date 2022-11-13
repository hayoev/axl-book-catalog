using Application.Admin.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Admin.Common.Exceptions;
using WebApi.Admin.Common.Responses;

namespace WebApi.Admin.Common.Filters
{
    public class ExceptionHandlerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // var httpContext = context.HttpContext;
            // var env = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
            ErrorResponse errorResponse = null;

            switch (context.Exception)
            {
                case ValidationException e:
                    errorResponse = new ErrorResponse(ErrorStatusCodeEnum.BAD_REQUEST, e.Message, e.Errors);
                    context.Result = new OkObjectResult(errorResponse);
                    context.ExceptionHandled = true;
                    break;
                case FluentValidation.ValidationException e:
                    errorResponse = new ErrorResponse(ErrorStatusCodeEnum.BAD_REQUEST, e.Message);
                    context.Result = new OkObjectResult(errorResponse);
                    context.ExceptionHandled = true;
                    break;
                case LogicException e:
                    errorResponse = new ErrorResponse(ErrorStatusCodeEnum.BAD_REQUEST, e.Message);
                    context.Result = new OkObjectResult(errorResponse);
                    context.ExceptionHandled = true;
                    break;
                case AccessForbiddenException _:
                    errorResponse = new ErrorResponse(ErrorStatusCodeEnum.BAD_REQUEST, "Доступ запрещен!");
                    context.Result = new OkObjectResult(errorResponse);
                    context.ExceptionHandled = true;
                    break;
                case UnauthorizedException _:
                    errorResponse = new ErrorResponse(ErrorStatusCodeEnum.ERROR_AUTH,"Ошибка авторизации");
                    context.Result = new OkObjectResult(errorResponse);
                    context.ExceptionHandled = true;
                    break;
                case TokenExpiredException _:
                    errorResponse = new ErrorResponse(ErrorStatusCodeEnum.TOKEN_EXPIRED,"Срок токена истек");
                    context.Result = new OkObjectResult(errorResponse);
                    context.ExceptionHandled = true;
                    break;
                case RefreshTokenExpiredException _:
                    errorResponse = new ErrorResponse(ErrorStatusCodeEnum.TOKEN_REFRESH_EXPIRED,"Срок токена истек");
                    context.Result = new OkObjectResult(errorResponse);
                    context.ExceptionHandled = true;
                    break;
                default:
                    break;
            }
        }
    }
}