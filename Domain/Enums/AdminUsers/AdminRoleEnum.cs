using System;
using Domain.Common;
using Domain.Common.BaseEnums;

namespace Domain.Enums.AdminUsers
{
    public class AdminRoleEnum : BaseEnum<AdminUserEnum, Guid>
    {
        public static AdminRoleEnum Sadmin { get; } =
            new AdminRoleEnum(Guid.Parse("e36f983b-95cd-11eb-9cfc-5254000c2f33"), "Администратор");

        protected AdminRoleEnum(Guid val, string name) : base(val, name)
        {
        }
    }
}