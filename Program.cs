using OrderMatchingEngine.Models;
using OrderMatchingEngine.Services;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Order Matching Engine Started...");
        var orderBook = new OrderBook();

        orderBook.AddOrder(new Order
        {
            Side = OrderSide.Buy,
            Price = 100,
            Quantity = 10
        });
        orderBook.AddOrder(new Order
        {
            Side = OrderSide.Buy,
            Price = 100,
            Quantity = 5
        });

        orderBook.AddOrder(new Order
        {
            Side = OrderSide.Sell,
            Price = 105,
            Quantity = 5
        });

        orderBook.Print();
        orderBook.GetBestBuy();
        orderBook.GetBestSell();

    }
}