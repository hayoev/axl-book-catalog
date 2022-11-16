using System;
using Domain.Common.BaseEnums;

namespace Domain.Enums.Users
{
    public class UserEnum : BaseEnum<UserEnum, Guid>
    {
        public static UserEnum TestUser { get; } =
            new UserEnum(Guid.Parse("b1b6e9e5-749b-4fde-8ba2-ba4e253268f3"), "Тестовый пользователь");

        protected UserEnum(Guid val, string name) : base(val, name)
        {
        }
    }
}