namespace WebApi.Admin.Common.Responses
{
    public class Meta
    {
        public object Pagination { get; set; }

        public string TemporaryToken { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public bool? IsAuth { get; set; }
        
    }
}