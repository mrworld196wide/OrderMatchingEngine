using OrderMatchingEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMatchingEngine.Services
{
    public class MatchingEngine
    {
        private readonly OrderBook _orderBook;

        public MatchingEngine(OrderBook orderBook)
        {
            _orderBook = orderBook;
        }

        public List<Trade> ProcessOrder(Order incomingOrder)
        {
            var trades = new List<Trade>();

            if (incomingOrder.Side == OrderSide.Buy)
            {
                MatchBuyOrder(incomingOrder, trades);
            }
            else
            {
                MatchSellOrder(incomingOrder, trades);
            }

            if (incomingOrder.Quantity > 0)
            {
                _orderBook.AddOrder(incomingOrder);
            }

            return trades;
        }

        private void MatchBuyOrder(Order buyOrder, List<Trade> trades)
        {
            while (buyOrder.Quantity > 0)
            {
                var bestSell = _orderBook.GetBestSell();

                if (bestSell == null) break;

                //matching condition
                if (buyOrder.Price < bestSell.Price) break;

                int tradedQty = Math.Min(buyOrder.Quantity, bestSell.Quantity);

                var trade = new Trade
                {
                    BuyOrderId = buyOrder.Id,
                    SellOrderId = bestSell.Id,
                    Price = bestSell.Price,
                    Quantity = tradedQty
                };

                trades.Add(trade);

                buyOrder.Quantity -= tradedQty;
                bestSell.Quantity -= tradedQty;

                
                if (bestSell.Quantity == 0)
                {
                    _orderBook.RemoveOrder(bestSell);
                }
            }
        }

        private void MatchSellOrder(Order sellOrder, List<Trade> trades)
        {
            while (sellOrder.Quantity > 0)
            {
                var bestBuy = _orderBook.GetBestBuy();

                if (bestBuy == null) break;

                //matching condition
                if (sellOrder.Price > bestBuy.Price) break;

                int tradedQty = Math.Min(sellOrder.Quantity, bestBuy.Quantity);

                var trade = new Trade
                {
                    BuyOrderId = bestBuy.Id,
                    SellOrderId = sellOrder.Id,
                    Price = bestBuy.Price,
                    Quantity = tradedQty
                };

                trades.Add(trade);
                sellOrder.Quantity -= tradedQty;
                bestBuy.Quantity -= tradedQty;

                if (bestBuy.Quantity == 0)
                {
                    _orderBook.RemoveOrder(bestBuy);
                }
            }
        }
    }
}
