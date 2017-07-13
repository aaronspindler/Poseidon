namespace Poseidon.Models.Old
{
    public class TradeInfo
    {
        /// <summary>
        ///     Total cost of closed portion of position(quote currency).
        /// </summary>
        public decimal? CCost;

        /// <summary>
        ///     Total fee of closed portion of position(quote currency).
        /// </summary>
        public decimal? CFee;

        /// <summary>
        ///     Total margin freed in closed portion of position(quote currency).
        /// </summary>
        public decimal? CMargin;

        /// <summary>
        ///     Total cost of order (quote currency).
        /// </summary>
        public decimal Cost;

        /// <summary>
        ///     Average price of closed portion of position(quote currency).
        /// </summary>
        public decimal? CPrice;

        /// <summary>
        ///     Total fee of closed portion of position(quote currency).
        /// </summary>
        public decimal? CVol;

        /// <summary>
        ///     Total fee (quote currency).
        /// </summary>
        public decimal Fee;

        /// <summary>
        ///     Initial margin (quote currency).
        /// </summary>
        public decimal Margin;

        /// <summary>
        ///     Comma delimited list of miscellaneous info.
        ///     closing = trade closes all or part of a position.
        /// </summary>
        public string Misc;

        /// <summary>
        ///     Net profit/loss of closed portion of position(quote currency, quote currency scale).
        /// </summary>
        public decimal? Net;

        /// <summary>
        ///     Order responsible for execution of trade.
        /// </summary>
        public string OrderTxid;

        /// <summary>
        ///     Order type.
        /// </summary>
        public string OrderType;

        /// <summary>
        ///     Asset pair.
        /// </summary>
        public string Pair;

        /// <summary>
        ///     Position status(open/closed).
        /// </summary>
        public string PosStatus;

        /// <summary>
        ///     Average price order was executed at (quote currency).
        /// </summary>
        public decimal Price;

        /// <summary>
        ///     Unix timestamp of trade.
        /// </summary>
        public double Time;

        /// <summary>
        ///     List of closing trades for position(if available).
        /// </summary>
        public string[] Trades;

        /// <summary>
        ///     Type of order (buy/sell).
        /// </summary>
        public string Type;

        /// <summary>
        ///     Volume (base currency).
        /// </summary>
        public decimal Vol;
    }
}