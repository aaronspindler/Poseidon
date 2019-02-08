#region

using System.Collections.Generic;

#endregion

namespace Poseidon.Models.FiatCurrency.EuropeanCentralBank
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