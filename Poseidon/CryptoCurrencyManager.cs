#region

using System.Collections.Generic;
using Poseidon.Models.CryptoCurrency;

#endregion

namespace Poseidon
{
    public class CryptoCurrencyManager
    {
        private readonly List<KrakenCurrencyResponse> krakenData;
        private Kraken _kraken = new Kraken();

        public CryptoCurrencyManager(Kraken kraken)
        {
            _kraken = kraken;
        }

        public void GetKrakenData()
        {
        }
    }
}