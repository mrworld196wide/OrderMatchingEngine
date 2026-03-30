using OrderMatchingEngine.Models;

namespace OrderMatchingEngine.Services
{
    public class OrderBook
    {
        private Dictionary<decimal, List<Order>> buyOrders;
        private Dictionary<decimal, List<Order>> sellOrders;

        public OrderBook()
        {
            buyOrders = new Dictionary<decimal, List<Order>>();
            sellOrders = new Dictionary<decimal, List<Order>>();
        }

        public void AddOrder(Order order)
        {
            var book = order.Side == OrderSide.Buy ? buyOrders : sellOrders;
            if (!book.ContainsKey(order.Price))
            {
                book[order.Price] = new List<Order>();
            }

            book[order.Price].Add(order);
            Console.WriteLine("--- ORDER PLACED ------ ");
        }

        public void Print()
        {
            Console.WriteLine("BUY ORDERS:");
            foreach (var kvp in buyOrders)
            {
                Console.WriteLine($"Price: {kvp.Key}, Orders: {kvp.Value.Count}");
            }

            Console.WriteLine("SELL ORDERS:");
            foreach (var kvp in sellOrders)
            {
                Console.WriteLine($"Price:{kvp.Key}, Orders: {kvp.Value.Count}");
            }
        }
    }
}
