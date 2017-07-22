namespace Poseidon.Models.Old
{
    public class GetWithdrawStatusResult
    {
        /// <summary>
        ///     Asset class.
        /// </summary>
        public string Aclass;

        /// <summary>
        ///     Amount withdrawn.
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
        ///     Name of the withdrawal method used.
        /// </summary>
        public string Method;

        /// <summary>
        ///     Reference id.
        /// </summary>
        public string RefId;

        /// <summary>
        ///     Status of withdrawal.
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

        //onhold = withdrawal is on hold pending review.
        //return = a return transaction initiated by Kraken; it cannot be canceled.
        //cancel-denied = cancelation requested but was denied.
        //canceled = canceled.
        //cancel-pending = cancelation requested.

        //status-prop = additional status properties(if available).
    }
}