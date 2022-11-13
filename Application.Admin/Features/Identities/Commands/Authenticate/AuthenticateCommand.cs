using MediatR;

namespace Application.Admin.Features.Identities.Commands.Authenticate
{
    public class AuthenticateCommand : IRequest<AuthenticateViewModel>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}