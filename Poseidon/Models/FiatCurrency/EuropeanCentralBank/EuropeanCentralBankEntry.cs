using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Poseidon.Models.FiatCurrency.EuropeanCentralBank
{
    [DynamoDBTable("Poseidon.ECB_Data")]
    public class EuropeanCentralBankEntry
    {
        public EuropeanCentralBankEntry()
        {
            _valuations = new Dictionary<string, double>();
        }

        public EuropeanCentralBankEntry(string date, Dictionary<string, double> valuations)
        {
            Date = date;
            _valuations = new Dictionary<string, double>();
        }

        [DynamoDBHashKey] private string Date { get; set; }
        [DynamoDBProperty] private Dictionary<string, double> _valuations { get; set; }

        public Dictionary<string, double> GetValuations()
        {
            return _valuations;
        }

        public void SetDate(string date)
        {
            Date = date;
        }

        public void AddValuation(string pair, double value)
        {
            _valuations.Add(pair, value);
        }

        public void SetValuations(Dictionary<string, double> currencies)
        {
            _valuations = currencies;
        }
    }
}