    namespace OrderMatchingEngine.Models
{
    public class Trade
    {
        public string BuyOrderId { get; set; }
        public string SellOrderId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
