#region

using System.Collections.Generic;

#endregion

namespace Poseidon.Models.Kraken
{
    public class OrderBook
    {
        public List<OrderBookOrder> asks { get; set; }
        public List<OrderBookOrder> bids { get; set; }
    }
}