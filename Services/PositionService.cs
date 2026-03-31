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
            Console.WriteLine($"NetQty: {_position.NetQuantity}");
            Console.WriteLine($"AvgPrice: {_position.AvgPrice}");
            Console.WriteLine($"RealizedPnL: {_position.RealizedPnL}");
            Console.WriteLine($"UnrealizedPnL: {_position.UnrealizedPnL}");
        }
    }
}
