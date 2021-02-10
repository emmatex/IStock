using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class StockStateDto : BaseEntityDto
    {
        public string Name { get; set; }
        public bool Expired { get; set; }
        public bool Damaged { get; set; }
        public bool IsDefault { get; set; }
        public bool IssueAction { get; set; }
        public bool ReturnAction { get; set; }
        public bool ReceiveAction { get; set; }
        public bool CloseToExpiry { get; set; }
        public bool SellableAction { get; set; }
        public bool IsSystemDefined { get; set; }
        public bool AdjustmentAction { get; set; }
    }

    public class CreateUpdateStockStateDto
    {
        [Required]
        [MaxLength(128, ErrorMessage = "The name shouldn't have more than 200 characters.")]
        public string Name { get; set; }
        public bool Expired { get; set; }
        public bool Damaged { get; set; }
        public bool IsDefault { get; set; }
        public bool IssueAction { get; set; }
        public bool ReturnAction { get; set; }
        public bool ReceiveAction { get; set; }
        public bool CloseToExpiry { get; set; }
        public bool SellableAction { get; set; }
        public bool IsSystemDefined { get; set; }
        public bool AdjustmentAction { get; set; }
    }

}
