using OrderMatchingEngine.Models;

namespace OrderMatchingEngine.Services
{
    public class OrderBook
    {
        private SortedDictionary<decimal, Queue<Order>> buyOrders;
        private SortedDictionary<decimal, Queue<Order>> sellOrders;

        public OrderBook()
        {
            buyOrders = new SortedDictionary<decimal, Queue<Order>>(Comparer<decimal>.Create((a, b) => b.CompareTo(a)));
            sellOrders = new SortedDictionary<decimal, Queue<Order>>();
        }

        public void AddOrder(Order order)
        {
            var book = order.Side == OrderSide.Buy ? buyOrders : sellOrders;
            if (!book.ContainsKey(order.Price))
            {
                book[order.Price] = new Queue<Order>();
            }

            book[order.Price].Enqueue(order);
            Console.WriteLine($"x---- ORDER PLACED for Price:{order.Price} and Qty:{order.Quantity} x----");
        }

        public void Print()
        {
            Console.WriteLine("BUY ORDERS:");
            foreach (var kvp in buyOrders)
            {
                Console.WriteLine($"Price: {kvp.Key}, Count: {kvp.Value.Count}");
            }

            Console.WriteLine("SELL ORDERS:");
            foreach (var kvp in sellOrders)
            {
                Console.WriteLine($"Price: {kvp.Key}, Count: {kvp.Value.Count}");
            }
        }
    }
}
