using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Common.BaseEntities;
using Domain.Common.SoftDeletes;
using Domain.Entities.AdminUsers;
using Domain.Entities.Books;

namespace Domain.Entities.Authors
{
    public class Author : AdminAuditableBaseEntity, ISoftDeleted
    {
        public string Fullname { get; set; }
        public string Bio { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public ICollection<Book> Books { get; set; } = new Collection<Book>();
        public DateTime? DeletedDateTime { get; set; }
        public Guid? DeletedByUserId { get; set; }
        public AdminUser DeletedByUser { get; set; }
    }
}