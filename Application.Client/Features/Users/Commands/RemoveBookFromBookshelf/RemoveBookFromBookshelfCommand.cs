using System;
using MediatR;

namespace Application.Client.Features.Users.Commands.RemoveBookFromBookshelf
{
    public class RemoveBookFromBookshelfCommand : IRequest<Guid>
    {
        public Guid BookId { get; set; }
       
    }
}