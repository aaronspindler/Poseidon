using System.Collections.Generic;

namespace Poseidon.Models
{
    public class GetTradesHistoryResult
    {
        public Dictionary<string, TradeInfo> Trades;
        public int Count;
    }
}