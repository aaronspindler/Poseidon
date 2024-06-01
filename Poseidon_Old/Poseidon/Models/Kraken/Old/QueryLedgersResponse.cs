#region

using System.Collections.Generic;

#endregion

namespace Poseidon.Models.Old
{
    public class QueryLedgersResponse : ResponseBase
    {
        public Dictionary<string, LedgerInfo> Result;
    }
}