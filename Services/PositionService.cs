using OrderMatchingEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMatchingEngine.Services
{
    public class PositionService
    {
        private Position _position;

        public PositionService()
        {
            _position = new Position();
        }

        public Position GetPosition()
        {
            return _position;
        }
        public void Print()
        {
            Console.WriteLine("###############################");
            Console.WriteLine($"NetQty: {_position.NetQuantity}");
            Console.WriteLine($"AvgPrice: {_position.AvgPrice}");
            //Console.WriteLine($"RealizedPnL: {_position.RealizedPnL}");
            //Console.WriteLine($"UnrealizedPnL: {_position.UnrealizedPnL}");
            Console.WriteLine("###############################");
        }
        public void UpdatePosition(Trade trade, OrderSide side)
        {
            if (side == OrderSide.Buy)
            {
                int newQty = _position.NetQuantity + trade.Quantity;

                _position.AvgPrice =
                    ((_position.NetQuantity * _position.AvgPrice) +
                     (trade.Quantity * trade.Price)) / newQty;

                _position.NetQuantity = newQty;
            }
            else
            {
                _position.NetQuantity -= trade.Quantity;

                if (_position.NetQuantity == 0)
                    _position.AvgPrice = 0;
            }
        }
    }
}
