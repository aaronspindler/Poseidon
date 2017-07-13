using System.Collections.Generic;

namespace Poseidon.Models.Old
{
    public class GetRecentSpreadResult
    {
        /// <summary>
        ///     Id to be used as since when polling for new spread data
        /// </summary>
        public long Last;

        public Dictionary<string, List<SpreadItem>> Spread;
    }
}