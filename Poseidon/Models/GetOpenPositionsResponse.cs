using System.Collections.Generic;

namespace Poseidon.Models
{
    public class GetOpenPositionsResponse : ResponseBase
    {
        public Dictionary<string, PositionInfo> Result;
    }
}