using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2.DataModel;

namespace Poseidon.Models.FiatCurrency.Fixer
{
    [DynamoDBTable("Poseidon.FIXER_Data")]
    public class FixerEntry
    {
        [DynamoDBHashKey] string EntryID { get; set; }
        [DynamoDBProperty] string _date { get; set; }
        [DynamoDBProperty] private Dictionary<string, double> _valuations { get; set; }

        public FixerEntry()
        {
            EntryID = Guid.NewGuid().ToString("N");
            _valuations = new Dictionary<string, double>();
        }

        public void SetDate(string date)
        {
            _date = date;
        }

        public void AddValuation(string key, double value)
        {
            _valuations.Add(key, value);
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();
            text.Append(_date);
            foreach (var valuation in _valuations)
            {
                text.Append(" " + valuation.Key + " : " + valuation.Value);
            }

            return text.ToString();
        }
    }
}