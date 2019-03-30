#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Poseidon.Misc;
using Poseidon.Models.FiatCurrency.EuropeanCentralBank;

#endregion

namespace Poseidon.Fiat
{
    /// <summary>
    ///     The manager for interacting with the European Central Bank
    /// </summary>
    public class EuropeanCentralBankManager
    {
        private EuropeanCentralBankEntry entry;
        private Dictionary<string, double> rebasedCurrencies;

        /// <summary>
        ///     Constructor for the EuropeanCentralBankManager
        /// </summary>
        public EuropeanCentralBankManager()
        {
            entry = new EuropeanCentralBankEntry();
        }

        /// <summary>
        ///     The main public method for getting and storing data from the European central bank
        /// </summary>
        public void GetFiatRates()
        {
            GetData();
            RebaseCurrency();
            AddToDatabase();
        }

        /// <summary>
        ///     Gets current data containing exchange rates for fiat currencies from the ECB (European Central Bank)
        /// </summary>
        private void GetData()
        {
            try
            {
                var client = new WebClient();
                var data = client.OpenRead("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
                var reader = new StreamReader(data);

                //Read the header and ignore it
                for (var i = 0; i < 7; i++) reader.ReadLine();


                //Read the date and ignore it
                var date = reader.ReadLine().Trim().Split('\'')[1];

                //Read and write the actual currency data
                var lines = new List<string>();
                for (var i = 0; i < 32; i++) lines.Add(reader.ReadLine().Trim());


                //Start the loop
                entry = new EuropeanCentralBankEntry();
                entry.SetDate(date);
                foreach (var x in lines)
                {
                    var split = x.Split('\'');
                    var name = split[1];
                    var rate = Convert.ToDouble(split[3]);

                    entry.AddValuation(name, rate);
                }

                reader.Close();
                Logger.WriteLine("Updated Fiat Rates from European Central Bank");
            }
            catch (Exception e)
            {
                Logger.WriteLine(e.Message);
            }
        }

        /// <summary>
        ///     Adds data retrieved from European Central Bank to DynamoDB
        /// </summary>
        private void AddToDatabase()
        {
            Database.CreateECBEntry(entry);
        }


        /// <summary>
        ///     Rebase all currency prices to be in CAD instead of Euro
        /// </summary>
        private void RebaseCurrency()
        {
            rebasedCurrencies = new Dictionary<string, double>();
            var mostRecentEntry = entry;
            if (mostRecentEntry == null) return;
            var euroBase = mostRecentEntry.GetValuations();


            rebasedCurrencies.Add("EUR", 1 / euroBase["CAD"]);

            foreach (var currency in euroBase)
            {
                var currencyName = currency.Key;
                var newValue = currency.Value / euroBase["CAD"];
                rebasedCurrencies.Add(currencyName, newValue);
            }

            entry.SetValuations(rebasedCurrencies);
        }
    }
}