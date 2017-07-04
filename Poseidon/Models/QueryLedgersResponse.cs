using System.Collections.Generic;

namespace Poseidon.Models
{
    public class QueryLedgersResponse : ResponseBase
    {
        public Dictionary<string, LedgerInfo> Result;
    }
}