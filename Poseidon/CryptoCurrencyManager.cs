#region

using System.Collections.Generic;
using Poseidon.Crypto;

#endregion

namespace Poseidon
{
    /// <summary>
    ///     Manages data and interactions with all cryptocurrency resources
    /// </summary>
    public class CryptoCurrencyManager
    {
        /// <summary>
        ///     Controller for Kraken
        /// </summary>
        private Kraken _kraken = new Kraken();

        /// <summary>
        ///     Constructor for CryptoCurrencyManager
        /// </summary>
        /// <param name="kraken">Kraken Object</param>
        public CryptoCurrencyManager(Kraken kraken)
        {
            _kraken = kraken;
        }
    }
}