using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Exceptions;
using Application.Admin.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.Features.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _dbContext.Authors.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken) ??
                         throw new LogicException("Author not found");
            _mapper.Map(request, author);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return author.Id;
        }
    }
}