#region

using System.Collections.Generic;

#endregion

namespace Poseidon.Models.Old
{
    public class QueryOrdersResponse : ResponseBase
    {
        public Dictionary<string, OrderInfo> Result;
    }
}