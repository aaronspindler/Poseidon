using System.Collections.Generic;

namespace Poseidon.Models.Old
{
    public class GetTickerResponse : ResponseBase
    {
        public Dictionary<string, Ticker> Result;
    }
}