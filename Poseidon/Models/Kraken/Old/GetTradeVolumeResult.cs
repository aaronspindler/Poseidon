﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Poseidon.Models.Old
{
    public class GetTradeVolumeResult
    {
        /// <summary>
        ///     Volume currency.
        /// </summary>
        public string Currency;

        /// <summary>
        ///     Fee tier info (if requested).
        /// </summary>
        public Dictionary<string, FeeInfo> Fees;

        /// <summary>
        ///     Maker fee tier info (if requested) for any pairs on maker/taker schedule.
        /// </summary>
        [JsonProperty(PropertyName = "fees_maker")] public Dictionary<string, FeeInfo> FeesMaker;

        /// <summary>
        ///     Current discount volume.
        /// </summary>
        public decimal Volume;
    }
}