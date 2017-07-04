using System.Collections.Generic;

namespace Poseidon.Models
{
    public class GetLedgerResult
    {
        public Dictionary<string, LedgerInfo> Ledger;
        public int Count;
    }
}