﻿namespace Poseidon.Models
{
    public class LedgerInfo
    {
        /// <summary>
        /// Reference id.
        /// </summary>
        public string Refid;

        /// <summary>
        /// Unix timestamp of ledger.
        /// </summary>
        public double Time;

        /// <summary>
        /// Type of ledger entry.
        /// </summary>
        public string Type;

        /// <summary>
        /// Asset class.
        /// </summary>
        public string Aclass;

        /// <summary>
        /// Asset.
        /// </summary>
        public string Asset;

        /// <summary>
        /// Transaction amount.
        /// </summary>
        public decimal Amount;

        /// <summary>
        /// Transaction fee.
        /// </summary>
        public decimal Fee;

        /// <summary>
        /// Resulting balance.
        /// </summary>
        public decimal Balance;
    }
}