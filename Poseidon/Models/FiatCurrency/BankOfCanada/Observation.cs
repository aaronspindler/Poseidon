using System;
using System.Collections.Generic;

namespace Poseidon.Models.FiatCurrency.BankOfCanada
{
    public class Observation
    {
        private DateTime _date;
        private Dictionary<string, double> _valuations;

        public Observation(DateTime d)
        {
            _valuations = new Dictionary<string, double>();
        }

        public double GetValue(string key)
        {
            return _valuations[key];
        }

        public void AddValue(string k, double v)
        {
            _valuations.Add(k, v);
        }

        public DateTime GetDate()
        {
            return _date;
        }
    }
}