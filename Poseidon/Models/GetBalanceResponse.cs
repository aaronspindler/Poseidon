using System.Collections.Generic;

namespace Poseidon.Models
{
    public class GetBalanceResponse : ResponseBase
    {
        public Dictionary<string, decimal> Result;
    }
}