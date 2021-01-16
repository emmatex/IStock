using System;

namespace Core.Entities
{
    public abstract class ChildEntity
    {
        public string Id { get; set; }
        public string Remark { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Measurement { get; set; }
        public string StockStateName { get; set; }
        public DateTimeOffset CreatedOn { get; set; } = DateTime.Now;
    }
}
