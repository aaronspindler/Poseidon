namespace Poseidon.Models.Old
{
    public class OrderDescription
    {
        /// <summary>
        ///     Conditional close order description (if conditional close set).
        /// </summary>
        public string Close;

        /// <summary>
        ///     Amount of leverage
        /// </summary>
        public string Leverage;

        /// <summary>
        ///     Order description.
        /// </summary>
        public string Order;

        /// <summary>
        ///     Order type (See Add standard order).
        /// </summary>
        public string OrderType;

        /// <summary>
        ///     Asset pair.
        /// </summary>
        public string Pair;

        /// <summary>
        ///     Primary price.
        /// </summary>
        public decimal Price;

        /// <summary>
        ///     Secondary price
        /// </summary>
        public decimal Price2;

        /// <summary>
        ///     Type of order (buy/sell).
        /// </summary>
        public string Type;
    }
}