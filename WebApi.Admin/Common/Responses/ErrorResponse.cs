using System.Collections.Generic;

namespace WebApi.Admin.Common.Responses
{
    public class ErrorResponse : ResponseBase<string>
    {
        public ErrorResponse(ErrorStatusCodeEnum errorStatusCode, string message = null,
            IDictionary<string, string[]> errors = null) : base(false, errorStatusCode, null, null, message, errors)
        {
        }
    }
}