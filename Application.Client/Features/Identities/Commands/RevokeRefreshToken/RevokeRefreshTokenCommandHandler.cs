using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Client.Common.Exceptions;
using Application.Client.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Client.Features.Identities.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, bool>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public RevokeRefreshTokenCommandHandler(IApplicationDbContext dbContext,
            IAuthenticatedUserService authenticatedUserService)
        {
            _dbContext = dbContext;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<bool> Handle(
            RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                                .FirstOrDefaultAsync(x => x.Id == _authenticatedUserService.UserId, cancellationToken)
                            ?? throw new LogicException("User Not Found!");

            user.RefreshToken = null;
            user.RefreshTokenExpireDateTime = null;
            user.LastLogoutDateTime = DateTime.Now;
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return true;
        }
    }
}