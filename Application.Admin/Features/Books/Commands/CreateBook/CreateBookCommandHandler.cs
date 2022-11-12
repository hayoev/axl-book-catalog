using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Interfaces;
using AutoMapper;
using Domain.Entities.Books;
using MediatR;

namespace Application.Admin.Features.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);
            book.Id = Guid.NewGuid();
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return book.Id;
        }
    }
}