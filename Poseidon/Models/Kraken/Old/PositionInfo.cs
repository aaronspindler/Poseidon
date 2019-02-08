﻿#region

using Newtonsoft.Json;

#endregion

namespace Poseidon
{
    public class PositionInfo
    {
        /// <summary>
        ///     Opening cost of position (quote currency unless viqc set in oflags).
        /// </summary>
        public decimal Cost;

        /// <summary>
        ///     opening fee of position (quote currency).
        /// </summary>
        public decimal Fee;

        /// <summary>
        ///     Initial margin (quote currency).
        /// </summary>
        public decimal Margin;

        /// <summary>
        ///     Comma delimited list of miscellaneous info.
        /// </summary>
        public string Misc;

        /// <summary>
        ///     Unrealized profit/loss of remaining position (if docalcs requested.  quote currency, quote currency scale).
        /// </summary>
        public decimal Net;

        /// <summary>
        ///     Comma delimited list of order flags.
        /// </summary>
        public string OFlags;

        /// <summary>
        ///     Order responsible for execution of trade.
        /// </summary>
        public string OrderTxid;

        /// <summary>
        ///     Order type used to open position.
        /// </summary>
        public string OrderType;

        /// <summary>
        ///     Asset pair.
        /// </summary>
        public string Pair;

        /// <summary>
        ///     Unix timestamp of trade.
        /// </summary>
        public double Time;

        /// <summary>
        ///     Type of order used to open position (buy/sell).
        /// </summary>
        public string Type;

        /// <summary>
        ///     Current value of remaining position (if docalcs requested.  quote currency).
        /// </summary>
        public decimal Value;

        /// <summary>
        ///     Volume in quote currency.
        /// </summary>
        public decimal Viqc;

        /// <summary>
        ///     Position volume (base currency unless viqc set in oflags).
        /// </summary>
        public decimal Vol;

        /// <summary>
        ///     Position volume closed (base currency unless viqc set in oflags).
        /// </summary>
        [JsonProperty(PropertyName = "vol_closed")]
        public decimal VolClosed;
    }
}