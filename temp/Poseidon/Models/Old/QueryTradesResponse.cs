using System.Collections.Generic;

namespace Poseidon.Models.Old
{
    public class QueryTradesResponse : ResponseBase
    {
        public Dictionary<string, TradeInfo> Result;
    }
}