#region

using System.Collections.Generic;

#endregion

namespace Poseidon.Models.Kraken
{
    public class AccountBalance
    {
        public Dictionary<string, decimal> balances { get; set; }
    }
}