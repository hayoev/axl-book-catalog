using FluentValidation;

namespace Application.Admin.Features.Identities.Commands.SetPassword
{
    public class SetPasswordCommandValidator : AbstractValidator<SetPasswordCommand>
    {
        public SetPasswordCommandValidator()
        {
            RuleFor(x => x.Password)
                .NotNull()
                .MinimumLength(6)
                .MaximumLength(32)
                .Matches("^[0-9A-Za-z]*$")
                .WithMessage("Пароль должен состоится из букв и цифр");
            
            RuleFor(x => x.PasswordConfirmation).NotEmpty().NotNull();

            RuleFor(x => x)
                .Must(CheckPasswordConfirmation)
                .WithMessage("Пароль подтверждение введено неправильно!");
        }
        
        private static bool CheckPasswordConfirmation(SetPasswordCommand command)
        {
            return command.Password == command.PasswordConfirmation;
        }
    }
}