namespace Application.Admin.Features.Identities.Commands.Authenticate
{
    public class AuthenticateViewModel
    {
        public string FullName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TempToken { get; set; }
        public bool PasswordExpired { get; set; }
        public string[] Permissions { get; set; }
    }
}