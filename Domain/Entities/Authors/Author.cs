using System;
using Domain.Common.BaseEntities;

namespace Domain.Entities.Authors
{
    public class Author : AdminAuditableBaseEntity
    {
        public string Fullname { get; set; }
        public string Bio { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
    }
}