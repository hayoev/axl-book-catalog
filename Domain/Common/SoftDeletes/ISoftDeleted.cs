using System;

namespace Domain.Common.SoftDeletes
{
    public interface ISoftDeleted
    {
        public DateTime? DeletedDateTime { get; set; }
        public Guid? DeletedByUserId { get; set; }
    }
}