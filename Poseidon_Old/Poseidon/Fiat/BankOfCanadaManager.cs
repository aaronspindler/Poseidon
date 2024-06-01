#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Poseidon.Misc;
using Poseidon.Models.FiatCurrency.BankOfCanada;

#endregion

namespace Poseidon.Fiat
{
    /// <summary>
    ///     Manages data and interactions with Bank of Canada
    /// </summary>
    public class BankOfCanadaManager
    {
        /// <summary>
        ///     Variable for storing and retrieving the actual response from the bank of canada
        /// </summary>
        private BankOfCanadaResponse _response;

        /// <summary>
        ///     Public method for using BankOfCanadaManager no overloads
        /// </summary>
        public void GetFiatRates()
        {
            GetData();
            AddToDatabase();
        }

        /// <summary>
        ///     Public method for using BankOfCanadaManager with overloads
        /// </summary>
        /// <param name="start">Start datetime</param>
        /// <param name="end">End datetime</param>
        public void GetFiatRates(DateTime start, DateTime end)
        {
            GetData(start, end);
            AddToDatabase();
        }

        /// <summary>
        ///     Gets data from the furthest back Bank of Canada provides records for until Today then stores in database
        /// </summary>
        public void GetHistoricalData()
        {
            GetFiatRates(new DateTime(2015, 1, 1), DateTime.Today);
            Logger.WriteLine("Historical Data Retrieved and Processed");
        }

        /// <summary>
        ///     Gets currency price data from The Bank of Canada between two dates
        ///     All currencies are in CAD base
        /// </summary>
        /// <param name="start">The data to get data from</param>
        /// <param name="end">The date to get data to</param>
        private void GetData(DateTime start, DateTime end)
        {
            var response = new BankOfCanadaResponse();
            var startFormatted = start.Subtract(new TimeSpan(1, 0, 0, 0)).ToString("yyyy-MM-dd");
            var endFormatted = end.ToString("yyyy-MM-dd");


            var client = new WebClient();
            try
            {
                var apiAddress =
                    "https://www.bankofcanada.ca/valet/observations/FXAUDCAD,FXBRLCAD,FXCNYCAD,FXEURCAD,FXHKDCAD,FXINRCAD,FXIDRCAD,FXJPYCAD,FXMYRCAD,FXMXNCAD,FXNZDCAD,FXNOKCAD,FXPENCAD,FXRUBCAD,FXSARCAD,FXSGDCAD,FXZARCAD,FXKRWCAD,FXSEKCAD,FXCHFCAD,FXTWDCAD,FXTHBCAD,FXTRYCAD,FXGBPCAD,FXUSDCAD,FXVNDCAD/csv?start_date=" +
                    startFormatted + "&end_date=" + endFormatted;
                var data = client.OpenRead(apiAddress);
                var reader = new StreamReader(data);

                //Skip over line that just reads "TERMS AND CONDITIONS"
                reader.ReadLine();

                //Skip over line that just reads "https://www.bankofcanada.ca/terms/
                reader.ReadLine();

                //Skip empty line
                reader.ReadLine();

                //Skip over line that just reads "SERIES"
                reader.ReadLine();

                //Skip over line with column labels "id, label, description"
                reader.ReadLine();

                //Create a dictionary to put the series
                var series = new Dictionary<string, string>();


                //Loop through, parse, and add all series to list
                var seriesLine = reader.ReadLine();
                while (seriesLine != "")
                {
                    var splitLine = seriesLine.Split(",");
                    series.Add(splitLine[1], splitLine[2]);
                    //This will skip the blank line at the end of the data so theres no need to skip the blank line at the end of it
                    seriesLine = reader.ReadLine();
                }


                //Skip over line that just reads "OBSERVATIONS"
                reader.ReadLine();

                //Get line with all currency pairs
                var columnNames = reader.ReadLine();
                //Split the line into its respective pair names
                var currencyNames = columnNames.Split(",");
                var currencyNamesCleaned = new List<string>();
                for (var i = 1; i < currencyNames.Length; i++)
                    currencyNamesCleaned.Add(currencyNames[i].Substring(2, 3));

                //Read first observation line
                var observationsLine = reader.ReadLine();
                do
                {
                    //Some date ranges will result in 0 observations. Such as on a 1 day range on a weekend where there is no data (Bank is closed)
                    if (observationsLine == "") break;
                    var observationLineSplit = observationsLine.Split(",");
                    var entry = new BankOfCanadaEntry();
                    entry.SetDate(observationLineSplit[0]);
                    //var obs = new Observation(Convert.ToDateTime(observationLineSplit[0]));
                    for (var i = 1; i < observationLineSplit.Length; i++)
                        entry.AddValuation(currencyNamesCleaned[i - 1], Convert.ToDouble(observationLineSplit[i]));

                    response.AddEntry(entry);
                    observationsLine = reader.ReadLine();
                } while (observationsLine != "");

                _response = response;
                Logger.WriteLine("Updated Fiat Rates from Bank Of Canada");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        ///     Gets currency price data from The Bank Of Canada for the current day
        /// </summary>
        private void GetData()
        {
            GetData(DateTime.Today.AddDays(-1), DateTime.Today);
        }

        /// <summary>
        ///     Adds responses from Bank of Canada to database
        /// </summary>
        private void AddToDatabase()
        {
            foreach (var entry in _response.GetEntries())
            {
                Database.CreateBOCEntry(entry);
            }
        }

        /// <summary>
        ///     Returns the Bank of Canada response
        /// </summary>
        /// <returns>BankOfCanadaResponse</returns>
        public BankOfCanadaResponse GetResponse()
        {
            return _response;
        }
    }
}