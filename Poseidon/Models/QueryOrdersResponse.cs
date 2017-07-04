using System.Collections.Generic;

namespace Poseidon.Models
{
    public class QueryOrdersResponse : ResponseBase
    {
        public Dictionary<string, OrderInfo> Result;
    }
}