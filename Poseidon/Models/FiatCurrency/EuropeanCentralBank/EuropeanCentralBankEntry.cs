using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Poseidon.Models.FiatCurrency.EuropeanCentralBank
{
    [DynamoDBTable("ECB_Data")]
    public class EuropeanCentralBankEntry
    {
        [DynamoDBHashKey] private string EntryID { get; set; }
        [DynamoDBProperty] private string _date { get; set; }
        [DynamoDBProperty] private Dictionary<string, double> _valuations { get; set; }

        public EuropeanCentralBankEntry()
        {
            EntryID = Guid.NewGuid().ToString("N");
            _valuations = new Dictionary<string, double>();
        }

        public EuropeanCentralBankEntry(string date, Dictionary<string, double> valuations)
        {
            EntryID = Guid.NewGuid().ToString("N");
            _date = date;
            _valuations = new Dictionary<string, double>();
        }

        public Dictionary<string, double> GetValuations()
        {
            return _valuations;
        }

        public void SetDate(string date)
        {
            _date = date;
        }

        public void AddValuation(string pair, double value)
        {
            _valuations.Add(pair,value);
        }

        public void SetValuations(Dictionary<string, double> currencies)
        {
            _valuations = currencies;
        }
    }
}