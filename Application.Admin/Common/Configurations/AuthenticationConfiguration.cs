namespace Application.Admin.Common.Configurations
{
    public class AuthenticationConfiguration
    {
        public const string Key = "Authentication";

        public int TimeoutRefreshTokenMinutes { get; set; } = 30;
        public int TimeoutAccessTokenMinutes { get; set; } = 5;
        public int TimeoutTempTokenMinutes { get; set; } = 5;
        public  bool ? PasswordExpireControl { get; set; }
        public JwtParam Jwt { get; set; }
        public PasswordParam Password { get; set; }

    }

    public class JwtParam
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
    
    public class PasswordParam
    {
        public string GlobalSalt { get; set; }
        public int IntervalPasswordExpireDay { get; set; } = 30;
    }
}