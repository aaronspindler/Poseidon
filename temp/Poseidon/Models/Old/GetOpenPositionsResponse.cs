using System.Collections.Generic;

namespace Poseidon.Models.Old
{
    public class GetOpenPositionsResponse : ResponseBase
    {
        public Dictionary<string, PositionInfo> Result;
    }
}