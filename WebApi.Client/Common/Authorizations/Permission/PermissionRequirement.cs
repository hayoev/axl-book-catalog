using System;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Client.Common.Authorizations.Permission
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public Guid PermissionId { get;}

        public PermissionRequirement(Guid permissionId)
        {
            PermissionId = permissionId;
        }
    }
}