using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Exceptions;
using Application.Admin.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.Features.Authors.Queries.GetAuthorEdit
{
    public class GetAuthorEditQueryHandler : IRequestHandler<GetAuthorEditQuery, GetAuthorEditViewModel>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorEditQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GetAuthorEditViewModel> Handle(GetAuthorEditQuery request,
            CancellationToken cancellationToken)
        {
            return await _dbContext.Authors
                       .AsNoTracking()
                       .ProjectTo<GetAuthorEditViewModel>(_mapper.ConfigurationProvider)
                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ??
                   throw new LogicException("Author not found");
        }
    }
}