using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Interfaces;
using AutoMapper;
using Domain.Entities.Authors;
using MediatR;

namespace Application.Admin.Features.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateAuthorCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = _mapper.Map<Author>(request);
            author.Id = Guid.NewGuid();
            _dbContext.Authors.Add(author);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return author.Id;
        }
    }
}