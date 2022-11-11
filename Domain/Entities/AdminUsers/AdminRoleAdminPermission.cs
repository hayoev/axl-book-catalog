using System;
using Domain.Common.BaseEntities;

namespace Domain.Entities.AdminUsers
{
    public class AdminRoleAdminPermission : AdminAuditableBaseEntity
    {
        public Guid AdminRoleId { get; set; }
        public Guid AdminPermissionId { get; set; }
        public AdminRole AdminRole { get; set; }
        public AdminPermission AdminPermission { get; set; }
    }
}