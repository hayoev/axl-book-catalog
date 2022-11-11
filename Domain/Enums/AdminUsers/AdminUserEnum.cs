using System;
using Domain.Common;
using Domain.Common.BaseEnums;

namespace Domain.Enums.AdminUsers
{
    public class AdminUserEnum : BaseEnum<AdminUserEnum, Guid>
    {
        public static AdminUserEnum System { get; } =
            new AdminUserEnum(Guid.Parse("62010f17-95cd-11eb-9cfc-5254000c2f33"), "Системный");

        public static AdminUserEnum Sadmin { get; } =
            new AdminUserEnum(Guid.Parse("101fda50-9084-11eb-aef2-244bfee059a7"), "Администратор");
        
        protected AdminUserEnum(Guid val, string name) : base(val, name)
        {
        }
    }
}