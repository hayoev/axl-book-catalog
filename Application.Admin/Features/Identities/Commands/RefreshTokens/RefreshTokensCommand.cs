using MediatR;

namespace Application.Admin.Features.Identities.Commands.RefreshTokens
{
    public class RefreshTokensCommand : IRequest<RefreshTokensViewModel>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}