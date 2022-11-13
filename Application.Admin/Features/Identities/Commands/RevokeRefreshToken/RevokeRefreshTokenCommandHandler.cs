using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Exceptions;
using Application.Admin.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.Features.Identities.Commands.RevokeRefreshToken
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
            var adminUser = await _dbContext.AdminUsers
                                .FirstOrDefaultAsync(x => x.Id == _authenticatedUserService.UserId, cancellationToken)
                            ?? throw new LogicException("AdminUser Not Found!");

            adminUser.RefreshToken = null;
            adminUser.RefreshTokenExpireDateTime = null;
            adminUser.LastLogoutDateTime = DateTime.Now;
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return true;
        }
    }
}