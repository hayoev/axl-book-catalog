using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Common.BaseEntities;

namespace Domain.Entities.Users
{
    public class User : AdminAuditableBaseEntity
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public bool IsActive { get; set; }

        public ICollection<UserBookshelf> Bookshelf { get; set; } = new Collection<UserBookshelf>();
    }
}