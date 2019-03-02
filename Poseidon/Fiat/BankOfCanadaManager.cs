#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using MySql.Data.MySqlClient;
using Poseidon.Database;
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

                //Skip over label that just reads "TERMS AND CONDITIONS"
                reader.ReadLine();

                //Create URI for terms and conditions
                var termsAndConditions = reader.ReadLine();
                var termsURI = new Uri(termsAndConditions);

                //Skip empty line
                reader.ReadLine();

                //Skip over label that just reads "SERIES"
                reader.ReadLine();

                //Skip over column labels "id, label, description"
                reader.ReadLine();

                //Create a list to put the series
                var series = new List<Series>();

                //Loop through, parse, and add all series to list
                var seriesLine = reader.ReadLine();
                while (seriesLine != "")
                {
                    var splitLine = seriesLine.Split(",");
                    var currentSeries = new Series(splitLine[0], splitLine[1], splitLine[2]);
                    series.Add(currentSeries);
                    //This will skip the blank line at the end of the data so theres no need to skip the blank line at the end of it
                    seriesLine = reader.ReadLine();
                }


                //Skip over label that just reads "OBSERVATIONS"
                reader.ReadLine();

                //Get line with all currency pairs
                var columnNames = reader.ReadLine();
                //Split the line into its respective pair names
                var currencyNames = columnNames.Split(",");
                var currencyNamesCleaned = new List<string>();
                for (var i = 1; i < currencyNames.Length; i++)
                    currencyNamesCleaned.Add(currencyNames[i].Substring(2, 3));

                var observations = new List<Observation>();
                var observationsLine = reader.ReadLine();

                //Get all of the pair data
                do
                {
                    //Some date ranges will result in 0 observations. Such as on a 1 day range on a weekend where there is no data (Bank is closed)
                    if (observationsLine == "") break;
                    var observationLineSplit = observationsLine.Split(",");
                    var obs = new Observation(Convert.ToDateTime(observationLineSplit[0]));
                    for (var i = 1; i < observationLineSplit.Length; i++)
                        obs.AddValue(currencyNamesCleaned[i - 1], Convert.ToDouble(observationLineSplit[i]));

                    observations.Add(obs);
                    observationsLine = reader.ReadLine();
                } while (observationsLine != "");

                //Create the response
                _response = new BankOfCanadaResponse(series, observations);
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
            GetData(DateTime.Today, DateTime.Today);
        }

        /// <summary>
        ///     Adds responses from Bank of Canada to database
        /// </summary>
        private void AddToDatabase()
        {
            var responseToAdd = _response;
            var observations = responseToAdd.GetObservations();
            try
            {
                var conn = MySQLDatabase.GetMySqlConnection();
                conn.Open();
                for (var i = 0; i < observations.Count; i++)
                {
                    var cmd = conn.CreateCommand();
                    cmd.CommandText =
                        "INSERT into Fiat_BOC(Date, AUD, BRL, CHF, CNY, EUR, GBP, HKD, IDR, INR, JPY, KRW, MXN, MYR, NOK, NZD, PEN, RUB, SAR, SEK, SGD, THB, TRY, TWD, USD, VND, ZAR) VALUES(@Date, @AUD, @BRL, @CHF, @CNY, @EUR, @GBP, @HKD, @IDR, @INR, @JPY, @KRW, @MXN, @MYR, @NOK, @NZD, @PEN, @RUB, @SAR, @SEK, @SGD, @THB, @TRY, @TWD, @USD, @VND, @ZAR)";
                    cmd.Parameters.AddWithValue("@Date", observations[i].GetDate());
                    cmd.Parameters.AddWithValue("@AUD", observations[i].GetValue("AUD"));
                    cmd.Parameters.AddWithValue("@BRL", observations[i].GetValue("BRL"));
                    cmd.Parameters.AddWithValue("@CHF", observations[i].GetValue("CHF"));
                    cmd.Parameters.AddWithValue("@CNY", observations[i].GetValue("CNY"));
                    cmd.Parameters.AddWithValue("@EUR", observations[i].GetValue("EUR"));
                    cmd.Parameters.AddWithValue("@GBP", observations[i].GetValue("GBP"));
                    cmd.Parameters.AddWithValue("@HKD", observations[i].GetValue("HKD"));
                    cmd.Parameters.AddWithValue("@IDR", observations[i].GetValue("IDR"));
                    cmd.Parameters.AddWithValue("@INR", observations[i].GetValue("INR"));
                    cmd.Parameters.AddWithValue("@JPY", observations[i].GetValue("JPY"));
                    cmd.Parameters.AddWithValue("@KRW", observations[i].GetValue("KRW"));
                    cmd.Parameters.AddWithValue("@MXN", observations[i].GetValue("MXN"));
                    cmd.Parameters.AddWithValue("@MYR", observations[i].GetValue("MYR"));
                    cmd.Parameters.AddWithValue("@NOK", observations[i].GetValue("NOK"));
                    cmd.Parameters.AddWithValue("@NZD", observations[i].GetValue("NZD"));
                    cmd.Parameters.AddWithValue("@PEN", observations[i].GetValue("PEN"));
                    cmd.Parameters.AddWithValue("@RUB", observations[i].GetValue("RUB"));
                    cmd.Parameters.AddWithValue("@SAR", observations[i].GetValue("SAR"));
                    cmd.Parameters.AddWithValue("@SEK", observations[i].GetValue("SEK"));
                    cmd.Parameters.AddWithValue("@SGD", observations[i].GetValue("SGD"));
                    cmd.Parameters.AddWithValue("@THB", observations[i].GetValue("THB"));
                    cmd.Parameters.AddWithValue("@TRY", observations[i].GetValue("TRY"));
                    cmd.Parameters.AddWithValue("@TWD", observations[i].GetValue("TWD"));
                    cmd.Parameters.AddWithValue("@USD", observations[i].GetValue("USD"));
                    cmd.Parameters.AddWithValue("@VND", observations[i].GetValue("VND"));
                    cmd.Parameters.AddWithValue("@ZAR", observations[i].GetValue("ZAR"));
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (MySqlException mysqlE)
                    {
                        if (mysqlE.Message.Contains("Duplicate entry")) continue;
                        Console.WriteLine(mysqlE.Message);
                    }
                    catch (Exception e)
                    {
                        Logger.WriteLine(e.Message);
                    }
                }

                conn.Close();
                Logger.WriteLine("Added fiat rates to database from Bank of Canada");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
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