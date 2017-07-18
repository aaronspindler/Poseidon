using System.Collections.Generic;

namespace Poseidon.Models
{
    public class OrderBook
    {
        public List<OrderBookOrder> asks { get; set; }
        public List<OrderBookOrder> bids { get; set; }
    }
}