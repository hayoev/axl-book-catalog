using System.Collections.Generic;

namespace WebApi.Client.Common.Responses
{
    public abstract class ResponseBase<TData>
    {
        protected ResponseBase(bool status, ErrorStatusCodeEnum? errorStatusCode = null, TData data = default,
            Meta meta = default, string message = null, IDictionary<string, string[]> errors = null)
        {
            Status = status;
            Data = data;
            Errors = errors;
            Message = message;
            ErrorCode = (short?)errorStatusCode;
            Meta = meta;
        }

        public bool Status { get; }
        public short? ErrorCode { get; }
        public string Message { get; }
        public IDictionary<string, string[]> Errors { get; }
        public TData Data { get; }

        public Meta Meta { get; }
    }
}