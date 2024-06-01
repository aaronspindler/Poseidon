#region

using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

#endregion

namespace Poseidon.Models.FiatCurrency.BankOfCanada
{
    /// <summary>
    ///     The object that holds the data for a response from the Bank Of Canada API
    /// </summary>
    [DynamoDBTable("Poseidon.BOC_Data")]
    public class BankOfCanadaEntry
    {
        public BankOfCanadaEntry()
        {
            _valuations = new Dictionary<string, double>();
        }

        public BankOfCanadaEntry(string date)
        {
            Date = date;
            _valuations = new Dictionary<string, double>();
        }

        [DynamoDBHashKey] private string Date { get; set; }
        [DynamoDBProperty] private Dictionary<string, double> _valuations { get; set; }

        public void SetDate(string date)
        {
            Date = date;
        }

        public void AddValuation(string key, double value)
        {
            _valuations.Add(key, value);
        }
    }
}