namespace WebApi.Client.Common.Responses
{
    public class SuccessResponse<TData> : ResponseBase<TData>
    {
        public SuccessResponse(TData data, Meta meta=null, string message = null)
            : base(true, null, data, meta, message)
        {
        }
    }
}