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
    [DynamoDBTable("BOC_Data")]
    public class BankOfCanadaEntry
    {
        [DynamoDBHashKey] private string EntryID { get; set; }
        [DynamoDBProperty] private string _date { get; set; }
        [DynamoDBProperty] private Dictionary<string, double> _valuations { get; set; }

        public BankOfCanadaEntry()
        {
            EntryID = Guid.NewGuid().ToString("N");
            _valuations = new Dictionary<string, double>();
        }

        public BankOfCanadaEntry(string date, Dictionary<string, double> valuations)
        {
            EntryID = Guid.NewGuid().ToString("N");
            _date = date;
            _valuations = new Dictionary<string, double>();
        }

        public void SetDate(string date)
        {
            _date = date;
        }

        public void AddValuation(string pair, double value)
        {
            _valuations.Add(pair,value);
        }
    }
}