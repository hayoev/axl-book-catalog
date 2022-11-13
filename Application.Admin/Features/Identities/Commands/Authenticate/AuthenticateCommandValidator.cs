using FluentValidation;

namespace Application.Admin.Features.Identities.Commands.Authenticate
{
    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}