using Xunit;
using OrderMatchingEngine.Models;
using OrderMatchingEngine.Services;

namespace OrderMatchingEngine.Tests
{
    public class MatchingEngineTests
    {
        [Fact]
        public void Should_Match_Buy_And_Sell_Order()
        {
            var orderBook = new OrderBook();
            var engine = new MatchingEngine(orderBook);

            engine.ProcessOrder(new Order { Side = OrderSide.Sell, Price = 95, Quantity = 5 });
            var trades = engine.ProcessOrder(new Order { Side = OrderSide.Buy, Price = 100, Quantity = 10 });

            Assert.Single(trades);
            Assert.Equal(5, trades[0].Quantity);
            Assert.Equal(95, trades[0].Price);
        }

        [Fact]
        public void Should_Not_Match_When_Price_Does_Not_Cross()
        {
            var orderBook = new OrderBook();
            var engine = new MatchingEngine(orderBook);

            engine.ProcessOrder(new Order { Side = OrderSide.Sell, Price = 110, Quantity = 5 });
            var trades = engine.ProcessOrder(new Order { Side = OrderSide.Buy, Price = 100, Quantity = 10 });

            Assert.Empty(trades);
        }

        [Fact]
        public void Should_Partially_Fill_Order()
        {
            var orderBook = new OrderBook();
            var engine = new MatchingEngine(orderBook);

            engine.ProcessOrder(new Order { Side = OrderSide.Sell, Price = 95, Quantity = 3 });
            var trades = engine.ProcessOrder(new Order { Side = OrderSide.Buy, Price = 100, Quantity = 10 });

            Assert.Single(trades);
            Assert.Equal(3, trades[0].Quantity);
        }

        [Fact]
        public void Should_Cancel_Order_Successfully()
        {
            var orderBook = new OrderBook();
            var engine = new MatchingEngine(orderBook);

            var order = new Order { Side = OrderSide.Buy, Price = 100, Quantity = 10 };
            engine.ProcessOrder(order);
            orderBook.CancelOrder(order.Id);

            var trades = engine.ProcessOrder(new Order { Side = OrderSide.Sell, Price = 95, Quantity = 5 });
            Assert.Empty(trades);
        }

        [Fact]
        public void Should_Modify_And_Rematch()
        {
            var orderBook = new OrderBook();
            var engine = new MatchingEngine(orderBook);

            var sellOrder = new Order { Side = OrderSide.Sell, Price = 110, Quantity = 5 };
            engine.ProcessOrder(sellOrder);

            orderBook.ModifyOrder(sellOrder.Id, 95, 5);

            var trades = engine.ProcessOrder(new Order { Side = OrderSide.Buy, Price = 100, Quantity = 5 });
            Assert.Single(trades); 
        }
    }
}