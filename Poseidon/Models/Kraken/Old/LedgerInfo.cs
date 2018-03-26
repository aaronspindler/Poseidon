namespace Poseidon.Models.Old
{
    public class LedgerInfo
    {
        /// <summary>
        ///     Asset class.
        /// </summary>
        public string Aclass;

        /// <summary>
        ///     Transaction amount.
        /// </summary>
        public decimal Amount;

        /// <summary>
        ///     Asset.
        /// </summary>
        public string Asset;

        /// <summary>
        ///     Resulting balance.
        /// </summary>
        public decimal Balance;

        /// <summary>
        ///     Transaction fee.
        /// </summary>
        public decimal Fee;

        /// <summary>
        ///     Reference id.
        /// </summary>
        public string Refid;

        /// <summary>
        ///     Unix timestamp of ledger.
        /// </summary>
        public double Time;

        /// <summary>
        ///     Type of ledger entry.
        /// </summary>
        public string Type;
    }
}