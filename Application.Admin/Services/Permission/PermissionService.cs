using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Admin.Common.Exceptions;
using Application.Admin.Common.Interfaces;
using Domain.Enums.AdminUsers;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.Services.Permission
{
    public class PermissionService : IPermissionService
    {
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IApplicationDbContext _dbContext;

        public PermissionService(IApplicationDbContext dbContext,
            IAuthenticatedUserService authenticatedUserService)
        {
            _dbContext = dbContext;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<bool> Can(Guid permissionId)
        {
            var adminUser = await _dbContext.AdminUsers
                .Include(p => p.AdminUserAdminRoles)
                .ThenInclude(p => p.AdminRole.AdminRoleAdminPermissions)
                .Select(a => new { a.Id, a.AdminUserAdminRoles })
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == _authenticatedUserService.UserId);

            if (_authenticatedUserService.UserId == default)
                return false;

            if (adminUser.AdminUserAdminRoles.Any(a => a.AdminRoleId == AdminRoleEnum.Sadmin.Value))
                return true;

            if (adminUser.AdminUserAdminRoles.Any(a =>
                    a.AdminRole.AdminRoleAdminPermissions.Any(a1 => a1.AdminPermissionId == permissionId)))
                return true;

            return false;
        }

        public async Task<bool> Can(string permissionCode)
        {
            var permission = await _dbContext
                                 .AdminPermissions
                                 .FirstOrDefaultAsync(x => x.Code == permissionCode) ??
                             throw new LogicException($"Permission Not Found: {permissionCode}");

            return await Can(permission.Id);
        }
    }
}