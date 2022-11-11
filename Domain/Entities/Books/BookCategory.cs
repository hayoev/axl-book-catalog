using System;
using Domain.Common.BaseEntities;
using Domain.Entities.Categories;

namespace Domain.Entities.Books
{
    public class BookCategory : AdminAuditableBaseEntity
    {
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}