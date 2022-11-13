using System.Threading.Tasks;
using Application.Admin.Services.Permission;
using Microsoft.AspNetCore.Authorization;
using WebApi.Admin.Common.Exceptions;

namespace WebApi.Admin.Common.Authorizations.Permission
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _permissionService;

        public PermissionAuthorizationHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (_permissionService.Can(requirement.PermissionId).Result)
            {
                context.Succeed(requirement);
            }
            else
            {
                return Task.FromException(new AccessForbiddenException());
            }

            return Task.CompletedTask;
        }
    }
}