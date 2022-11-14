using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Client.Common.Exceptions;
using Application.Client.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Client.Features.Users.Commands.RemoveBookFromBookshelf
{
    public class RemoveBookFromBookshelfCommandHandler : IRequestHandler<RemoveBookFromBookshelfCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public RemoveBookFromBookshelfCommandHandler(IApplicationDbContext dbContext,
            IAuthenticatedUserService authenticatedUserService)
        {
            _dbContext = dbContext;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<Guid> Handle(RemoveBookFromBookshelfCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == request.BookId, cancellationToken) ??
                       throw new LogicException("Book not found");

            var bookshelf = await _dbContext.UserBookshelfs.FirstOrDefaultAsync(x =>
                                    x.BookId == book.Id && x.UserId == _authenticatedUserService.UserId,
                                cancellationToken) ??
                            throw new LogicException("Такая книга не нейдена в полке");

            _dbContext.UserBookshelfs.Remove(bookshelf);

            await _dbContext.SaveChangesAsync(cancellationToken);
            return book.Id;
        }
    }
}