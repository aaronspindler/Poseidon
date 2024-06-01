namespace Poseidon.Models.Old
{
    public class GetDepositStatusResult
    {
        /// <summary>
        ///     Asset class.
        /// </summary>
        public string Aclass;

        /// <summary>
        ///     Amount deposited.
        /// </summary>
        public decimal Amount;

        /// <summary>
        ///     Asset X-ISO4217-A3 code.
        /// </summary>
        public string Asset;

        /// <summary>
        ///     Fees paid.
        /// </summary>
        public decimal Fee;

        /// <summary>
        ///     Method transaction information.
        /// </summary>
        public string Info;

        /// <summary>
        ///     Name of the deposit method used.
        /// </summary>
        public string Method;

        /// <summary>
        ///     Reference id.
        /// </summary>
        public string RefId;

        /// <summary>
        ///     status of deposit
        /// </summary>
        public string Status;

        /// <summary>
        ///     Unix timestamp when request was made.
        /// </summary>
        public int Time;

        /// <summary>
        ///     Method transaction id.
        /// </summary>
        public string Txid;

        // status-prop = additional status properties(if available)
        //    return = a return transaction initiated by Kraken

        //    onhold = deposit is on hold pending review
    }
}