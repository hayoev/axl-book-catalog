using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Exceptions;
using Application.Admin.Common.Interfaces;
using AutoMapper;
using Domain.Entities.Books;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.Features.Books.Commands.AddBookToCategory
{
    public class AddBookToCategoryCommandHandler : IRequestHandler<AddBookToCategoryCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddBookToCategoryCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddBookToCategoryCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == request.BookId, cancellationToken) ??
                       throw new LogicException("Book not found");

            var category =
                await _dbContext.Categories.FirstOrDefaultAsync(b => b.Id == request.CategoryId, cancellationToken) ??
                throw new LogicException("Category not found");

            var issets = await _dbContext.BookCategories
                .FirstOrDefaultAsync(x => x.BookId == book.Id && x.CategoryId == category.Id, cancellationToken);
            if (issets != null)
                throw new LogicException("Книга уже в категории");


            var bookCategory = new BookCategory()
            {
                Id = Guid.NewGuid(),
                CategoryId = category.Id,
                BookId = category.Id,
            };

            _dbContext.BookCategories.Add(bookCategory);

            await _dbContext.SaveChangesAsync(cancellationToken);
            return bookCategory.Id;
        }
    }
}