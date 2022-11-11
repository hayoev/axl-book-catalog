using System;
using Domain.Common.BaseEntities;

namespace Domain.Entities.AdminUsers
{
    public class AdminUserAdminRole : AdminAuditableBaseEntity
    {
        public Guid AdminUserId { get; set; }
        public Guid AdminRoleId { get; set; }
        public AdminRole AdminRole { get; set; }
        public AdminUser AdminUser { get; set; }
    }
}