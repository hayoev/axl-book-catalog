using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Client.Common.Responses;

namespace WebApi.Client.Common.Filters
{
    public class ErrorValidationHandlerActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Select(x => new
                        {Key = x.Key.Replace("$.",""), Value = x.Value.Errors.Select(error => error.ErrorMessage).ToArray()})
                    .ToDictionary(x =>x.Key.Length > 0 ? (char.ToLower(x.Key[0]) + (x.Key.Length > 1 ? x.Key.Substring(1) : "")):
                        x.Key, x => x.Value);
                //.GroupBy(e => e.PropertyName, e => e.ErrorMessage).ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
                // context.Result = new OkObjectResult(new ErrorResponse(ErrorStatusCodeEnum.BAD_REQUEST, errors:messages));
                context.Result = new OkObjectResult(new ErrorResponse(ErrorStatusCodeEnum.BAD_REQUEST, errors:errors));
                // context.Result = new OkObjectResult(messages);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}