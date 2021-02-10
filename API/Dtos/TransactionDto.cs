using System;

namespace API.Dtos
{
    public abstract class TransactionParentDto : BaseEntityDto
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

    public abstract class TransactionDetailDto 
    {
        public string Id { get; set; }
        public string Remark { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Measurement { get; set; }
        public string StockStateName { get; set; }
    }
}
