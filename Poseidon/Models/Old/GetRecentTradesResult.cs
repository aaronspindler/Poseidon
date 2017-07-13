using System.Collections.Generic;

namespace Poseidon.Models.Old
{
    public class GetRecentTradesResult
    {
        /// <summary>
        ///     Id to be used as since when polling for new trade data.
        /// </summary>
        public long Last;

        public Dictionary<string, List<Trade>> Trades;
    }
}