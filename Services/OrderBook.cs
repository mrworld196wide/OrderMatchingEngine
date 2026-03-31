using OrderMatchingEngine.Models;

namespace OrderMatchingEngine.Services
{
    public class OrderBook
    {
        private SortedDictionary<decimal, Queue<Order>> buyOrders;
        private SortedDictionary<decimal, Queue<Order>> sellOrders;
        private Dictionary<string, Order> orderLookup;

        public OrderBook()
        {
            buyOrders = new SortedDictionary<decimal, Queue<Order>>(Comparer<decimal>.Create((a, b) => b.CompareTo(a)));
            sellOrders = new SortedDictionary<decimal, Queue<Order>>();
            orderLookup = new Dictionary<string, Order>();
        }

        public void AddOrder(Order order)
        {
            var book = order.Side == OrderSide.Buy ? buyOrders : sellOrders;
            if (!book.ContainsKey(order.Price))
            {
                book[order.Price] = new Queue<Order>();
            }

            book[order.Price].Enqueue(order);
            orderLookup[order.Id] = order;
            Console.WriteLine($"###### {order.Side.ToString().ToUpper()} @Price: {order.Price} @Qty: {order.Quantity}######");
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

        public Order? GetBestBuy()
        {
            if (buyOrders.Count == 0) return null;

            var bestPriceLevel = buyOrders.First();
            return bestPriceLevel.Value.Peek();
        }

        public Order? GetBestSell()
        {
            if (sellOrders.Count == 0) return null;

            var bestPriceLevel = sellOrders.First();
            return bestPriceLevel.Value.Peek();
        }

        public void RemoveOrder(Order order)
        {
            var book = order.Side == OrderSide.Buy ? buyOrders : sellOrders;
            if (!book.ContainsKey(order.Price)) return;

            var queue = book[order.Price];
            queue.Dequeue(); 

            if (queue.Count == 0)
            {
                book.Remove(order.Price);
            }
        }

        public void CancelOrder(string orderId)
        {
            if (!orderLookup.ContainsKey(orderId)) return;

            var order = orderLookup[orderId];
            var book = order.Side == OrderSide.Buy ? buyOrders : sellOrders;

            if (!book.ContainsKey(order.Price)) return;

            var queue = book[order.Price];
            var newQueue = new Queue<Order>();

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current.Id != orderId)
                {
                    newQueue.Enqueue(current);
                }
            }

            if (newQueue.Count > 0)
            {
                book[order.Price] = newQueue;
            }
            else
            {
                book.Remove(order.Price);
            }

            orderLookup.Remove(orderId);
        }
        public void ModifyOrder(string orderId, decimal newPrice, int newQty)
        {
            if (!orderLookup.ContainsKey(orderId)) return;

            var oldOrder = orderLookup[orderId];

            CancelOrder(orderId);

            var newOrder = new Order
            {
                Id = orderId,
                Side = oldOrder.Side,
                Price = newPrice,
                Quantity = newQty,
                Timestamp = DateTime.UtcNow
            };

            AddOrder(newOrder);
        }
    }
}
