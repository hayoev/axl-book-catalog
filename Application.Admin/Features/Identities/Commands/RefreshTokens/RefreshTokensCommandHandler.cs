using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Configurations;
using Application.Admin.Common.Exceptions;
using Application.Admin.Common.Interfaces;
using Application.Admin.Services.Token;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ValidationException = FluentValidation.ValidationException;

namespace Application.Admin.Features.Identities.Commands.RefreshTokens
{
    public class RefreshTokensCommandHandler : IRequestHandler<RefreshTokensCommand, RefreshTokensViewModel>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ITokenService _tokenService;
        private readonly AuthenticationConfiguration _authenticationConfiguration;
        public RefreshTokensCommandHandler(
            IApplicationDbContext dbContext, 
            ITokenService tokenService,
            AuthenticationConfiguration authenticationConfiguration)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _authenticationConfiguration = authenticationConfiguration;
        }

        public async Task<RefreshTokensViewModel> Handle(
            RefreshTokensCommand request, CancellationToken cancellationToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            var username = principal.Identity.Name;

            var adminUser = await _dbContext.AdminUsers
                .SingleOrDefaultAsync(u => u.Username == username, cancellationToken);

            if (adminUser == null)
                throw new ValidationException("User Not Found!");

            if (!adminUser.IsActive)
                throw new ValidationException("User Is Not Active!");
            
            if (adminUser.RefreshTokenExpireDateTime <= DateTime.Now)
                throw new RefreshTokenExpiredException();
            
            if (adminUser.RefreshToken != request.RefreshToken)
                throw new UnauthorizedException();

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            adminUser.RefreshToken = newRefreshToken;
            adminUser.RefreshTokenExpireDateTime = DateTime.Now.AddMinutes(_authenticationConfiguration.TimeoutRefreshTokenMinutes);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new RefreshTokensViewModel()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };
        }
    }
}