#region

using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

#endregion

namespace Poseidon.Models.FiatCurrency.EuropeanCentralBank
{
    [DynamoDBTable("ECB_Data")]
    public class EuropeanCentralBankResponse
    {
        public Dictionary<string, double> currencies;
        public DateTime date;
        public string EntryID;

        public EuropeanCentralBankResponse()
        {
            EntryID = Guid.NewGuid().ToString("N");
            currencies = new Dictionary<string, double>();
        }
    }
}