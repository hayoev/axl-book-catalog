using System;
using System.Threading.Tasks;

namespace Application.Admin.Services.Permission
{
    public interface IPermissionService
    {
        Task<bool> Can(Guid permissionId);
        Task<bool> Can(string permissionCode);
    }
}