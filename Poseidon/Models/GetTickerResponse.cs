using System.Collections.Generic;

namespace Poseidon.Models
{
    public class GetTickerResponse : ResponseBase
    {
        public Dictionary<string, Ticker> Result;
    }
}