#region

using System.Collections.Generic;

#endregion

namespace Poseidon.Models.Old
{
    public class GetLedgerResult
    {
        public int Count;
        public Dictionary<string, LedgerInfo> Ledger;
    }
}