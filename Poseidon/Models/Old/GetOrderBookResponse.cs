using System.Collections.Generic;

namespace Poseidon.Models.Old
{
    public class GetOrderBookResponse : ResponseBase
    {
        public Dictionary<string, OrderBook> Result;
    }
}