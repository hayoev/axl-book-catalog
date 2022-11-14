using FluentValidation;

namespace Application.Client.Features.Users.Commands.AddBookToBookshelf
{
    public class AddBookToBookshelfCommandValidator : AbstractValidator<AddBookToBookshelfCommand>
    {
        public AddBookToBookshelfCommandValidator()
        {
            RuleFor(x => x.BookId).NotEmpty();
        }
    }
}