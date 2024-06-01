namespace Poseidon.Models.Old
{
    public class GetWithdrawInfoResult
    {
        /// <summary>
        ///     Amount of fees that will be paid.
        /// </summary>
        public decimal Fee;

        /// <summary>
        ///     Maximum net amount that can be withdrawn right now.
        /// </summary>
        public decimal Limit;

        /// <summary>
        ///     Name of the withdrawal method that will be used
        /// </summary>
        public string Method;
    }
}