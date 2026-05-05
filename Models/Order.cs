
namespace OrderMatchingEngine.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public OrderSide Side { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
    public enum OrderSide
    {
        Buy,
        Sell
    }
}
    