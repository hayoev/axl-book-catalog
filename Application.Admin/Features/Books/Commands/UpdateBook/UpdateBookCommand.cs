using System;
using MediatR;

namespace Application.Admin.Features.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cover { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public short PublishYear { get; set; }
        public int PageCount { get; set; }
        public Guid CategoryId { get; set; }
    }
}