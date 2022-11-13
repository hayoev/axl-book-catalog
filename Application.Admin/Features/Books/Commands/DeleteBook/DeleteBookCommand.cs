using System;
using MediatR;

namespace Application.Admin.Features.Books.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}