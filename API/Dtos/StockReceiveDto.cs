using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class StockReceiveDto : TransactionParentDto
    {
        public string ReceiverName { get; set; }
        public string DriverName { get; set; }
        public string VehicleRegNo { get; set; }
    }

    public class SingleStockReceiveDto : StockReceiveDto
    {
        public ICollection<StockReceiveDetailDto> StockReceiveDetails { get; set; }
           = new List<StockReceiveDetailDto>();
    }

    public class CreateUpdateReceiveDto 
    {
        [Required]
        [MaxLength(200, ErrorMessage = "The receiver name shouldn't have more than 200 characters.")]
        public string ReceiverName { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "The cross reference shouldn't have more than 100 characters.")]
        public string CrossReference { get; set; }
        [MaxLength(200, ErrorMessage = "The driver name shouldn't have more than 200 characters.")]
        public string DriverName { get; set; }
        [MaxLength(50, ErrorMessage = "The vehicle reg no shouldn't have more than 50 characters.")]
        public string VehicleRegNo { get; set; }
        public ICollection<CreateUpdateReceiveDetailDto> StockReceiveDetails { get; set; }
          = new List<CreateUpdateReceiveDetailDto>();
    }

    public class StockReceiveDetailDto : TransactionDetailDto
    {
        public int BillQuantity { get; set; }
        public int ReceivedQuantity { get; set; }
    }

    public class CreateUpdateReceiveDetailDto : TransactionDetailDto
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Bill quantity must be at least one")]
        public int BillQuantity { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Received quantity must be at least one")]
        public int ReceivedQuantity { get; set; }
    }

}
