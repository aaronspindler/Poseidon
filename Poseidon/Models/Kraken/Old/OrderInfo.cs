﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Poseidon.Models.Old
{
    public class OrderInfo
    {
        /// <summary>
        ///     Unix timestamp of when order was closed
        /// </summary>
        public double? CloseTm;

        /// <summary>
        ///     Total cost (quote currency unless unless viqc set in oflags)
        /// </summary>
        public decimal Cost;

        /// <summary>
        ///     Order description info
        /// </summary>
        public OrderDescription Descr;

        /// <summary>
        ///     Unix timestamp of order end time (or 0 if not set)
        /// </summary>
        public double ExpireTm;

        /// <summary>
        ///     Total fee (quote currency)
        /// </summary>
        public decimal Fee;

        /// <summary>
        ///     Triggered limit price (quote currency, when limit based order type triggered)
        /// </summary>
        public decimal? LimitPrice;

        /// <summary>
        ///     Comma delimited list of miscellaneous info
        ///     stopped = triggered by stop price
        ///     touched = triggered by touch price
        ///     liquidated = liquidation
        ///     partial = partial fill
        /// </summary>
        public string Misc;

        /// <summary>
        ///     Comma delimited list of order flags
        ///     viqc = volume in quote currency
        ///     fcib = prefer fee in base currency (default if selling)
        ///     fciq = prefer fee in quote currency (default if buying)
        ///     nompp = no market price protection
        /// </summary>
        public string Oflags;

        /// <summary>
        ///     Unix timestamp of when order was placed
        /// </summary>
        public double OpenTm;

        /// <summary>
        ///     Average price (quote currency unless viqc set in oflags)
        /// </summary>
        public decimal Price;

        /// <summary>
        ///     Additional info on status (if any)
        /// </summary>
        public string Reason;

        /// <summary>
        ///     Referral order transaction id that created this order
        /// </summary>
        public string RefId;

        /// <summary>
        ///     Unix timestamp of order start time (or 0 if not set)
        /// </summary>
        public double StartTm;

        /// <summary>
        ///     Status of order
        ///     pending = order pending book entry
        ///     open = open order
        ///     closed = closed order
        ///     canceled = order canceled
        ///     expired = order expired
        /// </summary>
        public string Status;

        /// <summary>
        ///     Stop price (quote currency, for trailing stops)
        /// </summary>
        public decimal? StopPrice;

        /// <summary>
        ///     Array of trade ids related to order (if trades info requested and data available)
        /// </summary>
        public List<string> Trades = new List<string>();

        /// <summary>
        ///     User reference id
        /// </summary>
        public int? UserRef;

        /// <summary>
        ///     Volume of order (base currency unless viqc set in oflags)
        /// </summary>
        [JsonProperty(PropertyName = "vol")] public decimal Volume;

        /// <summary>
        ///     Volume executed (base currency unless viqc set in oflags)
        /// </summary>
        [JsonProperty(PropertyName = "vol_exec")]
        public decimal VolumeExecuted;
    }
}