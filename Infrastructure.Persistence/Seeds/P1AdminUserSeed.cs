using System;
using Domain.Enums.AdminUsers;
using System.Collections.Generic;
using Application.Admin.Common.Configurations;
using Application.Admin.Services.PasswordHasher;
using Domain.Entities.AdminUsers;
using Infrastructure.Persistence.Seeds.Base;

namespace Infrastructure.Persistence.Seeds
{
    public class P1AdminUserSeed : BaseSeed<AdminUser>
    {
        protected override bool ForceUpdate => true;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly AuthenticationConfiguration _authenticationConfiguration;

        public P1AdminUserSeed(IPasswordHasherService passwordHasher,
            AuthenticationConfiguration authenticationConfiguration)
        {
            _passwordHasher = passwordHasher;
            _authenticationConfiguration = authenticationConfiguration;
        }

        protected override IList<AdminUser> DataSeed()
        {
            var data = new List<AdminUser>();

            var (passwordHash, passwordSalt) = _passwordHasher
                .Create("admin123", _authenticationConfiguration.Password.GlobalSalt);

            data.Add(new AdminUser()
            {
                Id = AdminUserEnum.System.Value,
                Email = "systemib@axl.com",
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                PasswordExpireDateTime = DateTime.Now
                    .AddDays(_authenticationConfiguration.Password.IntervalPasswordExpireDay),
                Username = "system",
                FirstName = "system",
                LastName = "system",
                IsActive = true,
                CreatedByAdminUserId = null,
                CreatedDateTime = new DateTime(2022, 11, 13, 8, 47, 31, 256, DateTimeKind.Local).AddTicks(7253)
            });
            data.Add(new AdminUser()
            {
                Id = AdminUserEnum.Sadmin.Value,
                Email = "admin@alx.com",
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                PasswordExpireDateTime = DateTime.Now
                    .AddDays(_authenticationConfiguration.Password.IntervalPasswordExpireDay),
                Username = "admin",
                FirstName = "admin",
                LastName = "admin",
                IsActive = true,
                CreatedByAdminUserId = AdminUserEnum.System.Value,
                CreatedDateTime = new DateTime(2022, 11, 13, 8, 47, 31, 256, DateTimeKind.Local).AddTicks(7253)
            });

            return data;
        }
    }
}