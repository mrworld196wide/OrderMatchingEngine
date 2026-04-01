using OrderMatchingEngine.Models;
using OrderMatchingEngine.Services;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Order Matching Engine Started...");
        var orderBook = new OrderBook();
        var engine = new MatchingEngine(orderBook);
        var positionService = new PositionService();
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

        engine.ProcessOrder(new Order {Side = OrderSide.Sell, Price =95 ,Quantity=5 });

        var order1 = new Order { Side = OrderSide.Buy, Price = 100, Quantity = 10 };
        var trades =  engine.ProcessOrder(order1);

        //var buyTrades = engine.ProcessOrder(new Order { Side = OrderSide.Buy, Price = 100, Quantity = 10 });

        foreach (var trade in trades)
        {
            Console.WriteLine($"TRADE: {trade.Quantity} @ {trade.Price}");
            positionService.UpdatePosition(trade, OrderSide.Buy);
        }

        //orderBook.ModifyOrder(order1.Id, 105, 8);

        //orderBook.CancelOrder(order1.Id);

        //orderBook.Print();
        positionService.Print();

    }
}