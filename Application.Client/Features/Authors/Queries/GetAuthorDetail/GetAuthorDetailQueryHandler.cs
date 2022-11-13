using System.Threading;
using System.Threading.Tasks;
using Application.Client.Common.Exceptions;
using Application.Client.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Client.Features.Authors.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryHandler : IRequestHandler<GetAuthorDetailQuery, GetAuthorDetailViewModel>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GetAuthorDetailViewModel> Handle(GetAuthorDetailQuery request,
            CancellationToken cancellationToken)
        {
            return await _dbContext.Authors
                       .AsNoTracking()
                       .ProjectTo<GetAuthorDetailViewModel>(_mapper.ConfigurationProvider)
                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ??
                   throw new LogicException("Author not found");
        }
    }
}