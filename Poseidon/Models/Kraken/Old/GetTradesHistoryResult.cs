using System.Collections.Generic;

namespace Poseidon.Models.Old
{
    public class GetTradesHistoryResult
    {
        public int Count;
        public Dictionary<string, TradeInfo> Trades;
    }
}