using System;
using Domain.Entities.Users;

namespace Domain.Common.BaseEntities
{
    public class ClientAuditableBaseEntity : BaseEntity
    {
        public Guid CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }

        public User CreatedByUser { get; set; }
        public User UpdatedByUser { get; set; }
    }
}