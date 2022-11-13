using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Configurations;
using Application.Admin.Common.Interfaces;
using Application.Admin.Services.PasswordHasher;
using Application.Admin.Services.Token;
using Domain.Entities.AdminUsers;
using Domain.Enums.AdminUsers;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.Features.Identities.Commands.Authenticate
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

            var adminUser = await GetAdminUserByUsername(request.Username, cancellationToken);

            if (!_passwordHasherService.Verify(request.Password, adminUser.Password,
                    adminUser.PasswordSalt, _authenticationConfiguration.Password.GlobalSalt))
                throw new ValidationException("Login or password is incorrect");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, adminUser.Id.ToString()),
                new Claim(ClaimTypes.Name, adminUser.Username)
            };

            if (_authenticationConfiguration.PasswordExpireControl == false ||
                adminUser.PasswordExpireDateTime.HasValue && adminUser.PasswordExpireDateTime > DateTime.Now)
            {
                accessToken = _tokenService.GenerateAccessToken(claims);
                refreshToken = _tokenService.GenerateRefreshToken();

                adminUser.RefreshToken = refreshToken;
                adminUser.RefreshTokenExpireDateTime =
                    DateTime.Now.AddMinutes(_authenticationConfiguration.TimeoutRefreshTokenMinutes);
                adminUser.LastLoginDateTime = DateTime.Now;
                permissions = await GetPermissions(adminUser.Id);
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
                FullName = $"{adminUser.FirstName} {adminUser.MiddleName} {adminUser.LastName}",
                Permissions = permissions,
                PasswordExpired = passwordExpired,
                TempToken = tempToken
            };
        }

        private async Task<AdminUser> GetAdminUserByUsername(string username,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var adminUser = await _dbContext.AdminUsers
                .SingleOrDefaultAsync(x => x.Username == username, cancellationToken);

            if (adminUser == null)
                throw new ValidationException("Login or password is incorrect");

            if (!adminUser.IsActive)
                throw new ValidationException("User Is Not Active!");

            return adminUser;
        }

        private async Task<string[]> GetPermissions(Guid adminUserId)
        {
            var permissions = new List<string>();

            var adminUserAdminRoles = await _dbContext.AdminUserAdminRoles
                .Include(p => p.AdminRole)
                .ThenInclude(p => p.AdminRoleAdminPermissions)
                .ThenInclude(p => p.AdminPermission)
                .Where(a => a.AdminUserId == adminUserId)
                .Select(a => new { a.AdminRoleId, a.AdminRole })
                .AsNoTracking()
                .ToListAsync();

            if (adminUserAdminRoles.Any(p => p.AdminRoleId == AdminRoleEnum.Sadmin.Value))
            {
                return _dbContext.AdminPermissions.AsNoTracking().Select(p => p.Code).ToArray();
            }

            foreach (var adminUserAdminRole in adminUserAdminRoles)
            {
                if (adminUserAdminRole?.AdminRole?.AdminRoleAdminPermissions != null)
                {
                    permissions.AddRange(
                        adminUserAdminRole.AdminRole.AdminRoleAdminPermissions.Select(adminRoleAdminPermission =>
                            adminRoleAdminPermission.AdminPermission.Code)
                    );
                }
            }

            return permissions.ToArray();
        }
    }
}