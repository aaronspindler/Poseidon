#region

using System.Collections.Generic;

#endregion

namespace Poseidon.Models.Kraken
{
    public class AssetPairs
    {
        public object[] Error { get; set; }
        public Dictionary<string, Result> Result { get; set; }
    }

    public class Result
    {
        public long[] LeverageBuy { get; set; }
        public string Base { get; set; }
        public string AclassQuote { get; set; }
        public string AclassBase { get; set; }
        public string Altname { get; set; }
        public double[][] Fees { get; set; }
        public string FeeVolumeCurrency { get; set; }
        public double[][] FeesMaker { get; set; }
        public long LotMultiplier { get; set; }
        public string Lot { get; set; }
        public long[] LeverageSell { get; set; }
        public long LotDecimals { get; set; }
        public long MarginStop { get; set; }
        public long MarginCall { get; set; }
        public long PairDecimals { get; set; }
        public string Quote { get; set; }
    }
}