using System;
using System.Collections.Generic;

namespace Poseidon.Models.FiatCurrency.BankOfCanada
{
    public class BankOfCanadaResponse
    {
        private Uri _termsLink;
        private List<Series> _series;
        private List<Observation> _observations;

        public BankOfCanadaResponse(Uri termsLink, List<Series> series, List<Observation> observations)
        {
            _termsLink = termsLink ?? throw new ArgumentNullException(nameof(termsLink));
            _series = series ?? throw new ArgumentNullException(nameof(series));
            _observations = observations ?? throw new ArgumentNullException(nameof(observations));
        }

        public List<Observation> GetObservations()
        {
            return _observations;
        }
    }
}