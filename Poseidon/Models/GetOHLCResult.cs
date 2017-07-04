using System.Collections.Generic;

namespace Poseidon.Models
{
    public class GetOHLCResult
    {
        public Dictionary<string, List<OHLC>> Pairs;

        // <summary>
        /// Id to be used as since when polling for new, committed OHLC data.
        /// </summary>
        public long Last;
    }
}