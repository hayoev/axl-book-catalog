using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Common.BaseEntities;
using Domain.Entities.Books;

namespace Domain.Entities.Categories
{
    public class Category : AdminAuditableBaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<BookCategory> Books { get; set; } = new Collection<BookCategory>();
    }
}