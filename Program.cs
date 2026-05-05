using OrderMatchingEngine.Models;
using OrderMatchingEngine.Services;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Order Matching Engine Started");

        var orderBook = new OrderBook();
        var engine = new MatchingEngine(orderBook);
        var positionService = new PositionService();

        void Process(Order order)
        {
            Console.WriteLine("\n -------------------------------------------------------------");
            Console.WriteLine($"\n{order.Side} {order.Quantity} @ {order.Price}");

            var trades = engine.ProcessOrder(order);

            if (trades.Count == 0)
            {
                Console.WriteLine("No trades executed.");
            }

            foreach (var trade in trades)
            {
                Console.WriteLine($"TRADE: {trade.Quantity} @ {trade.Price}");
                positionService.UpdatePosition(trade, OrderSide.Buy);
            }

            Console.WriteLine("\nPosition:");
            positionService.Print();

            Console.WriteLine("\nOrderBook:");
            orderBook.Print();
        }

        Process(new Order { Side = OrderSide.Buy, Price = 110, Quantity = 10 });
        Process(new Order { Side = OrderSide.Buy, Price = 110, Quantity = 15 });
        Process(new Order { Side = OrderSide.Buy, Price = 105, Quantity = 5 });
        Process(new Order { Side = OrderSide.Buy, Price = 100, Quantity = 5 });

        Process(new Order { Side = OrderSide.Sell, Price = 102, Quantity = 45 });

        Console.WriteLine("\nDone");

        var b3 = new Order { Side = OrderSide.Buy, Price = 90, Quantity = 5 };
        var b4 = new Order { Side = OrderSide.Buy, Price = 91, Quantity = 5 };
        Console.WriteLine("\nCancel Order:");
        orderBook.CancelOrder(b4.Id);
        orderBook.Print();
        Console.WriteLine("\n Modify Order:");
        orderBook.ModifyOrder(b3.Id, 108, 7);
        orderBook.Print();
    }
}