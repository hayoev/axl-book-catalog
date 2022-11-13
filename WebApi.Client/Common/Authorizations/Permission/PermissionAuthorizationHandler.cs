using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Client.Common.Authorizations.Permission
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            //TODO without permission
            context.Succeed(requirement);
            
            return Task.CompletedTask;
        }
    }
}