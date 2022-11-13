using FluentValidation;

namespace Application.Client.Features.Identities.Commands.RefreshTokens
{
    public class RefreshTokensCommandValidator : AbstractValidator<RefreshTokensViewModel>
    {
        public RefreshTokensCommandValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty().NotNull();
            RuleFor(x => x.RefreshToken).NotEmpty().NotNull();
        }
    }
}