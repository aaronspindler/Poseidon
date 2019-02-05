// --- FiatCurrency.cs ---
//
// MIT License
//
// Copyright (c) 2018 Aaron Spindler - aaron@xnovax.net
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using MySql.Data.MySqlClient;
using Poseidon.Models.FiatCurrency;

namespace Poseidon
{
    public class FiatCurrencyManager
    {
        private readonly List<EuropeanCentralBankResponse> ecbData;
        private Dictionary<string, double> rebasedCurrencies;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Poseidon.FiatCurrency" /> class.
        /// </summary>
        public FiatCurrencyManager()
        {
            ecbData = new List<EuropeanCentralBankResponse>();
        }
        
        public void GetFiatRates()
        {
            GetEcbData();
            RebaseCurrency();
            AddtoDatabase();
        }

        /// <summary>
        ///     Gets current data containing exchange rates for fiat currencies from the ECB (European Central Bank)
        /// </summary>
        private void GetEcbData()
        {
            Directory.CreateDirectory("Fiat/ECB");
            var now = DateTime.Now.Ticks;
            var xmlDataFileName = string.Format(@"Fiat/ECB/{0}.txt", now);
            try
            {
                var client = new WebClient();
                var data = client.OpenRead("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
                var reader = new StreamReader(data);
                var writer = new StreamWriter(xmlDataFileName);
                //Read the header and ignore it
                for (var i = 0; i < 7; i++) reader.ReadLine();

                //Write the date
                writer.WriteLine(reader.ReadLine().Trim());

                //Read and write the actual currency data
                for (var i = 0; i < 32; i++) writer.WriteLine(reader.ReadLine().Trim());

                //Close reader and writers
                writer.Close();
                reader.Close();

                reader = new StreamReader(xmlDataFileName);

                //Skip over the date line
                var date = reader.ReadLine();
                var dateSplit = date.Split('\'');
                date = dateSplit[1];

                //Start the loop
                var response = new EuropeanCentralBankResponse();
                var txt = reader.ReadLine();
                while (txt != null)
                {
                    var split = txt.Split('\'');
                    var name = split[1];
                    var rate = Convert.ToDouble(split[3]);

                    response.currencies.Add(name, rate);
                    txt = reader.ReadLine();
                }

                reader.Close();
                ecbData.Add(response);
            }
            catch (Exception e)
            {
                Logger.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Adds current market rates to a personal database
        /// </summary>
        private void AddtoDatabase()
        {
            try
            {
                var conn = MySQLDatabase.GetMySqlConnection();
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText =
                    "INSERT INTO Fiat_ECB(Date, EUR, USD, JPY, BGN, CZK, DKK, GBP, HUF, PLN, RON, SEK, CHF, ISK, NOK, HRK, RUB, TRY, AUD, BRL, CAD, CNY, HKD, IDR, ILS, INR, KRW, MXN, MYR, NZD, PHP, SGD, THB, ZAR) VALUES(@Date, @EUR, @USD, @JPY, @BGN, @CZK, @DKK, @GBP, @HUF, @PLN, @RON, @SEK, @CHF, @ISK, @NOK, @HRK, @RUB, @TRY, @AUD, @BRL, @CAD, @CNY, @HKD, @IDR, @ILS, @INR, @KRW, @MXN, @MYR, @NZD, @PHP, @SGD, @THB, @ZAR)";
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@EUR", rebasedCurrencies["EUR"]);
                cmd.Parameters.AddWithValue("@USD", rebasedCurrencies["USD"]);
                cmd.Parameters.AddWithValue("@JPY", rebasedCurrencies["JPY"]);
                cmd.Parameters.AddWithValue("@BGN", rebasedCurrencies["BGN"]);
                cmd.Parameters.AddWithValue("@CZK", rebasedCurrencies["CZK"]);
                cmd.Parameters.AddWithValue("@DKK", rebasedCurrencies["DKK"]);
                cmd.Parameters.AddWithValue("@GBP", rebasedCurrencies["GBP"]);
                cmd.Parameters.AddWithValue("@HUF", rebasedCurrencies["HUF"]);
                cmd.Parameters.AddWithValue("@PLN", rebasedCurrencies["PLN"]);
                cmd.Parameters.AddWithValue("@RON", rebasedCurrencies["RON"]);
                cmd.Parameters.AddWithValue("@SEK", rebasedCurrencies["SEK"]);
                cmd.Parameters.AddWithValue("@CHF", rebasedCurrencies["CHF"]);
                cmd.Parameters.AddWithValue("@ISK", rebasedCurrencies["ISK"]);
                cmd.Parameters.AddWithValue("@NOK", rebasedCurrencies["NOK"]);
                cmd.Parameters.AddWithValue("@HRK", rebasedCurrencies["HRK"]);
                cmd.Parameters.AddWithValue("@RUB", rebasedCurrencies["RUB"]);
                cmd.Parameters.AddWithValue("@TRY", rebasedCurrencies["TRY"]);
                cmd.Parameters.AddWithValue("@AUD", rebasedCurrencies["AUD"]);
                cmd.Parameters.AddWithValue("@BRL", rebasedCurrencies["BRL"]);
                cmd.Parameters.AddWithValue("@CAD", rebasedCurrencies["CAD"]);
                cmd.Parameters.AddWithValue("@CNY", rebasedCurrencies["CNY"]);
                cmd.Parameters.AddWithValue("@HKD", rebasedCurrencies["HKD"]);
                cmd.Parameters.AddWithValue("@IDR", rebasedCurrencies["IDR"]);
                cmd.Parameters.AddWithValue("@ILS", rebasedCurrencies["ILS"]);
                cmd.Parameters.AddWithValue("@INR", rebasedCurrencies["INR"]);
                cmd.Parameters.AddWithValue("@KRW", rebasedCurrencies["KRW"]);
                cmd.Parameters.AddWithValue("@MXN", rebasedCurrencies["MXN"]);
                cmd.Parameters.AddWithValue("@MYR", rebasedCurrencies["MYR"]);
                cmd.Parameters.AddWithValue("@NZD", rebasedCurrencies["NZD"]);
                cmd.Parameters.AddWithValue("@PHP", rebasedCurrencies["PHP"]);
                cmd.Parameters.AddWithValue("@SGD", rebasedCurrencies["SGD"]);
                cmd.Parameters.AddWithValue("@THB", rebasedCurrencies["THB"]);
                cmd.Parameters.AddWithValue("@ZAR", rebasedCurrencies["ZAR"]);
                cmd.ExecuteNonQuery();
                conn.Close();
                Logger.WriteLine("Added fiat currency rates to database!");
            }
            catch (MySqlException ex)
            {
                if (ex.Message.Contains("Duplicate entry"))
                    Logger.WriteLine("Fiat Currency has already been recorded today.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        /// <summary>
        ///     Rebase all currency prices to be in CAD instead of Euro
        /// </summary>
        private void RebaseCurrency()
        {
            rebasedCurrencies = new Dictionary<string, double>();
            EuropeanCentralBankResponse lastResponse = ecbData.LastOrDefault();
            if (lastResponse == null) return;
            Dictionary<string, double> euroBase = lastResponse.currencies;


            rebasedCurrencies.Add("EUR", 1 / euroBase["CAD"]);

            foreach (var currency in euroBase)
            {
                string currencyName = currency.Key;
                double newValue = currency.Value / euroBase["CAD"];
                rebasedCurrencies.Add(currencyName, newValue);
            }
        }
    }
}