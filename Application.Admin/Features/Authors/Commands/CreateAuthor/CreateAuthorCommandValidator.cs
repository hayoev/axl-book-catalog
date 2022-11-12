using FluentValidation;

namespace Application.Admin.Features.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.Fullname).NotEmpty();
        }
    }
}