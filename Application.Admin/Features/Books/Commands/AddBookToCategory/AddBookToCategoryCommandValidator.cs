using FluentValidation;

namespace Application.Admin.Features.Books.Commands.AddBookToCategory
{
    public class AddBookToCategoryCommandValidator : AbstractValidator<AddBookToCategoryCommand>
    {
        public AddBookToCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.BookId).NotEmpty();
        }
    }
}