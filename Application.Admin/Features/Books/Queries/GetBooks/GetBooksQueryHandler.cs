using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Extensions;
using Application.Admin.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.Books;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.Features.Books.Queries.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, PaginateResult<GetBooksViewModel>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly GetBooksSieveProcessor _sieveProcessor;

        public GetBooksQueryHandler(IApplicationDbContext dbContext, IMapper mapper,
            GetBooksSieveProcessor sieveProcessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PaginateResult<GetBooksViewModel>> Handle(GetBooksQuery request,
            CancellationToken cancellationToken)
        {
            var queryable = _dbContext.Books
                .Include(x=>x.Author)
                .Include(x=>x.BookCategories)
                .OrderByDescending(p => p.CreatedDateTime)
                .AsQueryable();

            queryable = (IOrderedQueryable<Book>)_sieveProcessor.Apply(request.SieveModel, queryable,
                applyPagination: false);

            return await queryable.AsNoTracking()
                .ProjectTo<GetBooksViewModel>(_mapper.ConfigurationProvider)
                .PaginateResultAsync(request.Page, cancellationToken: cancellationToken);
        }
    }
}