#region

using System.Collections.Generic;

#endregion

namespace Poseidon.Models.CryptoCurrency
{
    public class KrakenCurrencyResponse
    {
        public Dictionary<string, double> currencies;

        public KrakenCurrencyResponse()
        {
            currencies = new Dictionary<string, double>();
        }
    }
}