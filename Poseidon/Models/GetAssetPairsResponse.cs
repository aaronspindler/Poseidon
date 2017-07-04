using System.Collections.Generic;

namespace Poseidon.Models
{
    public class GetAssetPairsResponse : ResponseBase
    {
        public Dictionary<string, AssetPair> Result;
    }
}