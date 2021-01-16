using System;

namespace Core.Entities
{
    public abstract class ParentEntity : BaseEntity
    {
        public string Remark { get; set; }
        public bool Approved { get; set; }
        public bool Rejected { get; set; }
        public bool Submitted { get; set; }
        public string ApprovedBy { get; set; }
        public string DocumentNo { get; set; }
        public string LastUpdatedBy { get; set; }
        public string CrossReference { get; set; }
        public DateTimeOffset Approvedon { get; set; }
        public DateTimeOffset LastUpdatedOn { get; set; }
        public DateTimeOffset TransactionDate { get; set; } 
    }
}
