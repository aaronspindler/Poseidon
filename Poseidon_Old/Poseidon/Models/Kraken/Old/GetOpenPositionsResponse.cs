#region

using System.Collections.Generic;

#endregion

namespace Poseidon.Models.Old
{
    public class GetOpenPositionsResponse : ResponseBase
    {
        public Dictionary<string, PositionInfo> Result;
    }
}