using System.Collections.Generic;

namespace Poseidon.Models
{
    public class GetRecentTradesResult
    {
        public Dictionary<string, List<Trade>> Trades;

        /// <summary>
        /// Id to be used as since when polling for new trade data.
        /// </summary>
        public long Last;
    }
}