using System;
using MediatR;

namespace Application.Admin.Features.Books.Commands.AddBookToCategory
{
    public class AddBookToCategoryCommand : IRequest<Guid>
    {
        public Guid BookId { get; set; }
        
        public Guid CategoryId { get; set; }
    }
}