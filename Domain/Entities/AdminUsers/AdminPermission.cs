using System.Collections.Generic;
using Domain.Common.BaseEntities;

namespace Domain.Entities.AdminUsers
{
    public class AdminPermission : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<AdminRoleAdminPermission> AdminRoleAdminPermissions { get; set; }
    }
}