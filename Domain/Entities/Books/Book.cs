using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Common.BaseEntities;
using Domain.Entities.Authors;
using Domain.Entities.Categories;

namespace Domain.Entities.Books
{
    public class Book : AdminAuditableBaseEntity
    {
        public string Name { get; set; }
        public string Cover { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public short PublishYear { get; set; }
        public int PageCount { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}