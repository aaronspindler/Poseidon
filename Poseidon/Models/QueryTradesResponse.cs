using System.Collections.Generic;

namespace Poseidon.Models
{
    public class QueryTradesResponse : ResponseBase
    {
        public Dictionary<string, TradeInfo> Result;
    }
}