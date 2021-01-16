using System;

namespace Core.Entities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; } = DateTime.Now;
    }
}
