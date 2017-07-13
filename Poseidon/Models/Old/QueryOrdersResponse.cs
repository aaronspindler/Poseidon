using System.Collections.Generic;

namespace Poseidon.Models.Old
{
    public class QueryOrdersResponse : ResponseBase
    {
        public Dictionary<string, OrderInfo> Result;
    }
}