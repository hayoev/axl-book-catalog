using FluentValidation;

namespace Application.Admin.Features.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(x => x.Fullname).NotEmpty();
        }
    }
}