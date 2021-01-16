namespace Core.Entities
{
    public class StockReceiveDetail : ChildEntity
    {
        public int BillQuantity { get; set; }
        public int ReceivedQuantity { get; set; }
        public StockReceive StockReceive { get; set; }
        public string StockReceiveId { get; set; }
        public Product Product { get; set; }
        public string ProductId { get; set; }
        public StockState StockState { get; set; }
        public string StockStateId { get; set; }
    }
}
