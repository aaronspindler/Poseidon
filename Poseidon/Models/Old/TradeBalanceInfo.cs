﻿using Newtonsoft.Json;

namespace Poseidon.Models.Old
{
    public class TradeBalanceInfo
    {
        /// <summary>
        ///     Cost basis of open positions.
        /// </summary>
        [JsonProperty(PropertyName = "c")] public decimal CostBasis;

        /// <summary>
        ///     Equity = trade balance + unrealized net profit/loss.
        /// </summary>
        [JsonProperty(PropertyName = "e")] public decimal Equity;

        /// <summary>
        ///     Equivalent balance(combined balance of all currencies).
        /// </summary>
        [JsonProperty(PropertyName = "eb")] public decimal EquivalentBalance;

        /// <summary>
        ///     Current floating valuation of open positions.
        /// </summary>
        [JsonProperty(PropertyName = "v")] public decimal FloatingValutation;

        /// <summary>
        ///     Free margin = equity - initial margin(maximum margin available to open new positions).
        /// </summary>
        [JsonProperty(PropertyName = "mf")] public decimal FreeMargin;

        /// <summary>
        ///     Margin amount of open positions.
        /// </summary>
        [JsonProperty(PropertyName = "m")] public decimal MarginAmount;

        /// <summary>
        ///     Margin level = (equity / initial margin) * 100
        /// </summary>
        [JsonProperty(PropertyName = "ml")] public decimal MarginLevel;

        /// <summary>
        ///     Trade balance(combined balance of all equity currencies).
        /// </summary>
        [JsonProperty(PropertyName = "tb")] public decimal TradeBalance;

        /// <summary>
        ///     Unrealized net profit/loss of open positions.
        /// </summary>
        [JsonProperty(PropertyName = "n")] public decimal UnrealizedProfitAndLoss;
    }
}