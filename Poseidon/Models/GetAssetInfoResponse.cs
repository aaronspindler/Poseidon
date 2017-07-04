using System.Collections.Generic;

namespace Poseidon.Models
{
    public class GetAssetInfoResponse : ResponseBase
    {
        public Dictionary<string, AssetInfo> Result;
    }
}