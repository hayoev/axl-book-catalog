using System;
using MediatR;

namespace Application.Client.Features.Users.Commands.AddBookToBookshelf
{
    public class AddBookToBookshelfCommand : IRequest<Guid>
    {
        public Guid BookId { get; set; }
       
    }
}