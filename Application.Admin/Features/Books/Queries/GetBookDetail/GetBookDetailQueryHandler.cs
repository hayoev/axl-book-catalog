using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Exceptions;
using Application.Admin.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.Features.Books.Queries.GetBookDetail
{
    public class GetBookDetailQueryHandler : IRequestHandler<GetBookDetailQuery, GetBookDetailViewModel>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBookDetailQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GetBookDetailViewModel> Handle(GetBookDetailQuery request,
            CancellationToken cancellationToken)
        {
            return await _dbContext.Books
                       .Include(x=>x.Author)
                       .Include(x=>x.BookCategories)
                       .AsNoTracking()
                       .ProjectTo<GetBookDetailViewModel>(_mapper.ConfigurationProvider)
                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ??
                   throw new LogicException("Book not found");
        }
    }
}