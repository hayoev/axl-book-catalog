using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Common.BaseEntities;
using Domain.Common.SoftDeletes;
using Domain.Entities.AdminUsers;
using Domain.Entities.Authors;
using Domain.Entities.Categories;

namespace Domain.Entities.Books
{
    public class Book : AdminAuditableBaseEntity, ISoftDeleted
    {
        public string Name { get; set; }
        public string Cover { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public short PublishYear { get; set; }
        public int PageCount { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public Guid? DeletedByUserId { get; set; }
        public AdminUser DeletedByUser { get; set; }
        
        public ICollection<BookCategory> BookCategories { get; set; } = new Collection<BookCategory>();

    }
}