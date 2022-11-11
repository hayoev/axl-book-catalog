using System;
using Domain.Entities.AdminUsers;

namespace Domain.Common.BaseEntities
{
    public class AdminAuditableBaseEntity : BaseEntity
    {
        public Guid CreatedByAdminUserId { get; set; }
        public Guid? UpdatedByAdminUserId { get; set; }
        
        public AdminUser CreatedByAdminUser { get; set; }
        public AdminUser UpdatedByAdminUser { get; set; }
        
    }
}