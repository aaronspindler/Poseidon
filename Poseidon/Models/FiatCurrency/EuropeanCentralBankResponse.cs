using System;
using System.Collections.Generic;

namespace Poseidon.Models.FiatCurrency
{
    public class EuropeanCentralBankResponse
	{
		public List<Currency> currencies;
        public EuropeanCentralBankResponse()
        {
			 currencies = new List<Currency>();
        }
    }
}
