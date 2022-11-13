using MediatR;

namespace Application.Admin.Features.Identities.Commands.SetPassword
{
    public class SetPasswordCommand : IRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}