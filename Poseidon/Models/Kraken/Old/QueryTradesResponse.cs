#region

using System.Collections.Generic;

#endregion

namespace Poseidon.Models.Old
{
    public class QueryTradesResponse : ResponseBase
    {
        public Dictionary<string, TradeInfo> Result;
    }
}