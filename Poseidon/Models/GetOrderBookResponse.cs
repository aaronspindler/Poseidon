using System.Collections.Generic;

namespace Poseidon.Models
{
    public class GetOrderBookResponse : ResponseBase
    {
        public Dictionary<string, OrderBook> Result;
    }
}