using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Client.Common.Exceptions;
using Application.Client.Common.Interfaces;
using Domain.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Client.Features.Users.Commands.AddBookToBookshelf
{
    public class AddBookToBookshelfCommandHandler : IRequestHandler<AddBookToBookshelfCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public AddBookToBookshelfCommandHandler(IApplicationDbContext dbContext,
            IAuthenticatedUserService authenticatedUserService)
        {
            _dbContext = dbContext;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<Guid> Handle(AddBookToBookshelfCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == request.BookId, cancellationToken) ??
                       throw new LogicException("Book not found");

            var bookshelf = await _dbContext.UserBookshelfs.FirstOrDefaultAsync(x =>
                x.BookId == book.Id && x.UserId == _authenticatedUserService.UserId, cancellationToken);

            if (bookshelf != null)
                throw new LogicException("Книга уже находится на полке");

            _dbContext.UserBookshelfs.Add(new UserBookshelf()
            {
                Id = Guid.NewGuid(),
                BookId = book.Id,
                UserId = _authenticatedUserService.UserId
            });


            await _dbContext.SaveChangesAsync(cancellationToken);
            return book.Id;
        }
    }
}