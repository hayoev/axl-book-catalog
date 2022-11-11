using System;
using Domain.Common.BaseEntities;
using Domain.Entities.Books;

namespace Domain.Entities.Users
{
    public class UserBookshelf : ClientAuditableBaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}