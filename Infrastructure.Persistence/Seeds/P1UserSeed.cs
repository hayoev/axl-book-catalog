using System;
using Domain.Enums.AdminUsers;
using System.Collections.Generic;
using Application.Admin.Common.Configurations;
using Application.Admin.Services.PasswordHasher;
using Domain.Entities.Users;
using Domain.Enums.Users;
using Infrastructure.Persistence.Seeds.Base;

namespace Infrastructure.Persistence.Seeds
{
    public class P1UserSeed : BaseSeed<User>
    {
        protected override bool ForceUpdate => true;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly AuthenticationConfiguration _authenticationConfiguration;

        public P1UserSeed(IPasswordHasherService passwordHasher, AuthenticationConfiguration authenticationConfiguration)
        {
            _passwordHasher = passwordHasher;
            _authenticationConfiguration = authenticationConfiguration;
        }

        protected override IList<User> DataSeed()
        {
            var data = new List<User>();
            
            var (passwordHash, passwordSalt) = _passwordHasher
                .Create("user123", _authenticationConfiguration.Password.GlobalSalt);

            data.Add(new User()
            {
                Id = UserEnum.TestUser.Value,
                Email = "user.test@alx.com",
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                PasswordExpireDateTime = DateTime.Now
                .AddDays(_authenticationConfiguration.Password.IntervalPasswordExpireDay),
                Username = "user",
                FirstName = "user",
                LastName = "user",
                IsActive = true,
                CreatedByAdminUserId = AdminUserEnum.Sadmin.Value,
                CreatedDateTime = new DateTime(2022, 11, 13, 8, 47, 31, 256, DateTimeKind.Local).AddTicks(7253)
            });

            return data;
        }
    }
}