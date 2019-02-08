using System;
using System.Collections.Generic;

namespace Poseidon.Models.FiatCurrency.BankOfCanada
{
    public class BankOfCanadaResponse
    {
        private string _terms;
        private Uri _termsLink;
        private List<Series> _series;
        private List<Observation> _observations;

    }
}