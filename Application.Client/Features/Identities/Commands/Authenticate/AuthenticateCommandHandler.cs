using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Client.Common.Configurations;
using Application.Client.Common.Exceptions;
using Application.Client.Common.Interfaces;
using Application.Client.Common.PasswordHasher;
using Application.Client.Common.Token;
using Domain.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Client.Features.Identities.Commands.Authenticate
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateViewModel>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly AuthenticationConfiguration _authenticationConfiguration;

        public AuthenticateCommandHandler(IApplicationDbContext dbContext, ITokenService tokenService,
            IPasswordHasherService passwordHasherService, AuthenticationConfiguration authenticationConfiguration)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _passwordHasherService = passwordHasherService;
            _authenticationConfiguration = authenticationConfiguration;
        }

        public async Task<AuthenticateViewModel> Handle(AuthenticateCommand request,
            CancellationToken cancellationToken)
        {
            string accessToken = null;
            string refreshToken = null;
            string tempToken = null;
            string[] permissions = null;
            bool passwordExpired = false;

            var user = await GetUserByUsername(request.Username, cancellationToken);

            if (!_passwordHasherService.Verify(request.Password, user.Password,
                    user.PasswordSalt, _authenticationConfiguration.Password.GlobalSalt))
                throw new ValidationException("Login or password is incorrect");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            if (_authenticationConfiguration.PasswordExpireControl == false ||
                user.PasswordExpireDateTime.HasValue && user.PasswordExpireDateTime > DateTime.Now)
            {
                accessToken = _tokenService.GenerateAccessToken(claims);
                refreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpireDateTime =
                    DateTime.Now.AddMinutes(_authenticationConfiguration.TimeoutRefreshTokenMinutes);
                user.LastLoginDateTime = DateTime.Now;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                passwordExpired = true;
                tempToken = _tokenService.GenerateTempToken(claims);
            }

            return new AuthenticateViewModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                FullName = $"{user.FirstName} {user.MiddleName} {user.LastName}",
                PasswordExpired = passwordExpired,
                TempToken = tempToken
            };
        }

        private async Task<User> GetUserByUsername(string username,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var user = await _dbContext.Users
                .SingleOrDefaultAsync(x => x.Username == username, cancellationToken);

            if (user == null)
                throw new ValidationException("Login or password is incorrect");

            if (!user.IsActive)
                throw new ValidationException("User Is Not Active!");

            return user;
        }
    }
}