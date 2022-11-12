using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Extensions;
using Application.Admin.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.Authors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.Features.Authors.Queries.GetAuthors
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, PaginateResult<GetAuthorsViewModel>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly GetAuthorsSieveProcessor _sieveProcessor;

        public GetAuthorsQueryHandler(IApplicationDbContext dbContext, IMapper mapper,
            GetAuthorsSieveProcessor sieveProcessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PaginateResult<GetAuthorsViewModel>> Handle(GetAuthorsQuery request,
            CancellationToken cancellationToken)
        {
            var queryable = _dbContext.Authors
                .OrderByDescending(p => p.CreatedDateTime)
                .AsQueryable();

            queryable = (IOrderedQueryable<Author>)_sieveProcessor.Apply(request.SieveModel, queryable,
                applyPagination: false);

            return await queryable.AsNoTracking()
                .ProjectTo<GetAuthorsViewModel>(_mapper.ConfigurationProvider)
                .PaginateResultAsync(request.Page, cancellationToken: cancellationToken);
        }
    }
}