using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Common.BaseEntities;
using Domain.Entities.Books;

namespace Domain.Entities.Authors
{
    public class Author : AdminAuditableBaseEntity
    {
        public string Fullname { get; set; }
        public string Bio { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public ICollection<Book> Books { get; set; } = new Collection<Book>();
    }
}