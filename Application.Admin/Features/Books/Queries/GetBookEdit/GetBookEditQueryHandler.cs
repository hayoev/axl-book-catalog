using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Exceptions;
using Application.Admin.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.Features.Books.Queries.GetBookEdit
{
    public class GetBookEditQueryHandler : IRequestHandler<GetBookEditQuery, GetBookEditViewModel>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBookEditQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GetBookEditViewModel> Handle(GetBookEditQuery request,
            CancellationToken cancellationToken)
        {
            return await _dbContext.Books
                       .AsNoTracking()
                       .ProjectTo<GetBookEditViewModel>(_mapper.ConfigurationProvider)
                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ??
                   throw new LogicException("Book not found");
        }
    }
}