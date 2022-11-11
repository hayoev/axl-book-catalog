using System;
using System.Collections.Generic;
using Domain.Common.BaseEntities;

namespace Domain.Entities.AdminUsers
{
    public class AdminUser : AdminAuditableBaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public DateTime? LastLogoutDateTime { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDateTime { get; set; }
        public new Guid? CreatedByAdminUserId { get; set; }
        public ICollection<AdminUserAdminRole> AdminUserAdminRoles { get; set; }
    }
}