#region

using System;
using System.Collections.Generic;

#endregion

namespace Poseidon.Models.FiatCurrency.BankOfCanada
{
    /// <summary>
    ///     Stores the observation for a currency from Bank of Canada
    /// </summary>
    public class Observation
    {
        private readonly DateTime _date;
        private readonly Dictionary<string, double> _valuations;

        /// <summary>
        ///     Constructor for a observation
        /// </summary>
        /// <param name="d">The DateTime that the observation was made</param>
        public Observation(DateTime d)
        {
            _date = d;
            _valuations = new Dictionary<string, double>();
        }

        /// <summary>
        ///     Returns the value of the keypair for a specific observation
        /// </summary>
        /// <param name="key">The key pair for the valuation of that pair</param>
        /// <returns></returns>
        public double GetValue(string key)
        {
            try
            {
                return _valuations[key];
            }
            catch (Exception e)
            {
                Logger.WriteLine(e.ToString());
                throw;
            }
        }


        /// <summary>
        ///     Adds a currency pair with value to the dictionary
        /// </summary>
        /// <param name="k">Key for the currency pair</param>
        /// <param name="v">Value for a currency pair</param>
        public void AddValue(string k, double v)
        {
            _valuations.Add(k, v);
        }

        /// <summary>
        ///     Returns the date of an observation
        /// </summary>
        /// <returns>Date of Observation</returns>
        public DateTime GetDate()
        {
            return _date;
        }
    }
}