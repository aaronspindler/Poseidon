using System;
using System.Collections.Generic;

namespace Poseidon.Models.FiatCurrency.BankOfCanada
{
    /// <summary>
    /// The object that holds the data for a response from the Bank Of Canada API
    /// </summary>
    public class BankOfCanadaResponse
    {
        private Uri _termsLink;
        private List<Series> _series;
        private List<Observation> _observations;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="termsLink">Link to the terms and conditions</param>
        /// <param name="series">List of the different currency pairs</param>
        /// <param name="observations">List of all observations for those currency pairs</param>
        public BankOfCanadaResponse(Uri termsLink, List<Series> series, List<Observation> observations)
        {
            _termsLink = termsLink ?? throw new ArgumentNullException(nameof(termsLink));
            _series = series ?? throw new ArgumentNullException(nameof(series));
            _observations = observations ?? throw new ArgumentNullException(nameof(observations));
        }

        /// <summary>
        /// Returns the list of observations
        /// </summary>
        /// <returns>List of observations</returns>
        public List<Observation> GetObservations()
        {
            return _observations;
        }
    }
}