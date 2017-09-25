using System.Collections.Generic;

namespace Poseidon.Models.Old
{
    public class KrakenOrder
    {
        /// <summary>
        ///     Optional closing order to add to system when order gets filled:
        ///     close[ordertype] = order type
        ///     close[price] = price
        ///     close[price2] = secondary price
        /// </summary>
        public Dictionary<string, string> Close;

        // The following fields are set in AddOrder when the order was added successfully

        /// <summary>
        ///     Order description info.
        /// </summary>
        public AddOrderDescr Descr;

        /// <summary>
        ///     Expiration time (optional):
        ///     0 = no expiration(default)
        ///     +
        ///     <n>
        ///         = expire
        ///         <n>
        ///             seconds from now
        ///             <n> = unix timestamp of expiration time
        /// </summary>
        public int? ExpireTm;

        /// <summary>
        ///     Amount of leverage desired (optional.  default = none).
        /// </summary>
        public decimal? Leverage;

        /// <summary>
        ///     Comma delimited list of order flags (optional):
        ///     viqc = volume in quote currency(not available for leveraged orders)
        ///     fcib = prefer fee in base currency
        ///     fciq = prefer fee in quote currency
        ///     nompp = no market price protection
        ///     post = post only order(available when ordertype = limit)
        /// </summary>
        public string OFlags;

        /// <summary>
        ///     Order type:
        ///     market
        ///     limit(price = limit price)
        ///     stop-loss(price = stop loss price)
        ///     take-profit(price = take profit price)
        ///     stop-loss-profit(price = stop loss price, price2 = take profit price)
        ///     stop-loss-profit-limit(price = stop loss price, price2 = take profit price)
        ///     stop-loss-limit(price = stop loss trigger price, price2 = triggered limit price)
        ///     take-profit-limit(price = take profit trigger price, price2 = triggered limit price)
        ///     trailing-stop(price = trailing stop offset)
        ///     trailing-stop-limit(price = trailing stop offset, price2 = triggered limit offset)
        ///     stop-loss-and-limit(price = stop loss price, price2 = limit price)
        ///     settle-position
        /// </summary>
        public string OrderType;

        // Required fields first

        /// <summary>
        ///     Asset pair.
        /// </summary>
        public string Pair;

        // Optional fields

        /// <summary>
        ///     Price (optional.  dependent upon ordertype).
        /// </summary>
        public decimal? Price;

        /// <summary>
        ///     Secondary price (optional.  dependent upon ordertype).
        /// </summary>
        public decimal? Price2;

        /// <summary>
        ///     scheduled start time (optional):
        ///     0 = now(default)
        ///     +
        ///     <n>
        ///         = schedule start time
        ///         <n>
        ///             seconds from now
        ///             <n> = unix timestamp of start time
        /// </summary>
        public int? StartTm;

        /// <summary>
        ///     Array of transaction ids for order (if order was added successfully).
        /// </summary>
        public string[] Txid;

        /// <summary>
        ///     Type of order (buy/sell).
        /// </summary>
        public string Type;

        /// <summary>
        ///     User reference id.  32-bit signed number.  (optional).
        /// </summary>
        public int? UserRef;

        /// <summary>
        ///     Validate inputs only.  do not submit order (optional)
        /// </summary>
        public bool? Validate;

        /// <summary>
        ///     Order volume in lots.
        /// </summary>
        public decimal Volume;
    }
}