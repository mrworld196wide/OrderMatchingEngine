using OrderMatchingEngine.Models;
using OrderMatchingEngine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMatchingEngine.Tests
{
    public class PositionServiceTests
    {
        [Fact]
        public void Avg_Multiple_Buys()
        {
            var service = new PositionService();

            service.UpdatePosition(new Trade { Price = 100, Quantity = 10 }, OrderSide.Buy);
            service.UpdatePosition(new Trade { Price = 120, Quantity = 10 }, OrderSide.Buy);

            var pos = service.GetPosition();

            Assert.Equal(20, pos.NetQuantity);
            Assert.Equal(110, pos.AvgPrice);
        }

        [Fact]
        public void Reduce_Quantity_On_Sell()
        {
            var service = new PositionService();

            service.UpdatePosition(new Trade { Price = 100, Quantity = 10 }, OrderSide.Buy);
            service.UpdatePosition(new Trade { Price = 120, Quantity = 10 }, OrderSide.Buy);

            service.UpdatePosition(new Trade { Price = 130, Quantity = 5 }, OrderSide.Sell);

            var pos = service.GetPosition();

            Assert.Equal(15, pos.NetQuantity);
            Assert.Equal(110, pos.AvgPrice); 
        }

        [Fact]
        public void Zero_Reset_On_Full_Sell()
        {
            var service = new PositionService();

            service.UpdatePosition(new Trade { Price = 100, Quantity = 10 }, OrderSide.Buy);
            service.UpdatePosition(new Trade { Price = 120, Quantity = 10 }, OrderSide.Buy);

            service.UpdatePosition(new Trade { Price = 130, Quantity = 20 }, OrderSide.Sell);

            var pos = service.GetPosition();

            Assert.Equal(0, pos.NetQuantity);
            Assert.Equal(0, pos.AvgPrice);
        }

    }
}
