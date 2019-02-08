#region

using System.Collections.Generic;

#endregion

namespace Poseidon.Models.FiatCurrency
{
    public class EuropeanCentralBankResponse
    {
        public Dictionary<string, double> currencies;

        public EuropeanCentralBankResponse()
        {
            currencies = new Dictionary<string, double>();
        }
    }
}