using System;
using Domain.Common.BaseEnums;

namespace Domain.Enums.AdminUsers
{
    public class AdminPermissionEnum : BaseEnum<AdminPermissionEnum, Guid>
    {
        #region AdminUser

        public static AdminPermissionEnum BookList { get; } =
            new AdminPermissionEnum(Guid.Parse("3427fa6f-ae49-11eb-acc1-244bfee059a7"), "Список книг");

        public static AdminPermissionEnum BookEdit { get; } =
            new AdminPermissionEnum(Guid.Parse("bc1570ad-f8a3-4dc5-b75a-3bf6c83812e7"), "Редактировать книгу");

        public static AdminPermissionEnum BookCreate { get; } =
            new AdminPermissionEnum(Guid.Parse("4aee4be3-c050-4f6c-ac05-039a45f80d31"), "Добавить книгу");

        #endregion

        protected AdminPermissionEnum(Guid val, string name) : base(val, name)
        {
        }
    }
}