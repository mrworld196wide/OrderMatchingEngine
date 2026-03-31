using OrderMatchingEngine.Models;
using OrderMatchingEngine.Services;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Order Matching Engine Started...");
        //var orderBook = new OrderBook();

        //orderBook.AddOrder(new Order
        //{
        //    Side = OrderSide.Buy,
        //    Price = 100,
        //    Quantity = 10
        //});
        //orderBook.AddOrder(new Order
        //{
        //    Side = OrderSide.Buy,
        //    Price = 100,
        //    Quantity = 5
        //});

        //orderBook.AddOrder(new Order
        //{
        //    Side = OrderSide.Sell,
        //    Price = 105,
        //    Quantity = 5
        //});

        //orderBook.Print();
        //orderBook.GetBestBuy();
        //orderBook.GetBestSell();

        var orderBook = new OrderBook();
        var engine = new MatchingEngine(orderBook);

        var sellTrades =  engine.ProcessOrder(new Order{ Side = OrderSide.Sell, Price = 95, Quantity = 5});

        var buyTrades = engine.ProcessOrder(new Order { Side = OrderSide.Buy, Price = 100, Quantity = 10 });

        foreach (var trade in buyTrades)
        {
            Console.WriteLine($"TRADE: {trade.Quantity} @ {trade.Price}");
        }

    }
}