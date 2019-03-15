#region

using System;
using System.Collections.Generic;

#endregion

namespace Poseidon.Models.FiatCurrency.EuropeanCentralBank
{
    public class EuropeanCentralBankResponse
    {
        public Dictionary<string, double> currencies;
        public DateTime date;
        public string ID;

        public EuropeanCentralBankResponse()
        {
            ID = Guid.NewGuid().ToString("N");
            currencies = new Dictionary<string, double>();
        }
    }
}