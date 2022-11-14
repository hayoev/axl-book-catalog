using FluentValidation;

namespace Application.Client.Features.Users.Commands.RemoveBookFromBookshelf
{
    public class RemoveBookFromBookshelfCommandValidator : AbstractValidator<RemoveBookFromBookshelfCommand>
    {
        public RemoveBookFromBookshelfCommandValidator()
        {
            RuleFor(x => x.BookId).NotEmpty();
        }
    }
}