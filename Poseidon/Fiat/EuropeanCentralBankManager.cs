#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using MySql.Data.MySqlClient;
using Poseidon.Database;
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
        private readonly List<EuropeanCentralBankResponse> ecbData;
        private Dictionary<string, double> rebasedCurrencies;

        /// <summary>
        ///     Constructor for the EuropeanCentralBankManager
        /// </summary>
        public EuropeanCentralBankManager()
        {
            ecbData = new List<EuropeanCentralBankResponse>();
        }

        /// <summary>
        ///     The main public method for getting and storing data from the European central bank
        /// </summary>
        public void GetFiatRates()
        {
            GetData();
            RebaseCurrency();
            AddtoDatabase();
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
                var response = new EuropeanCentralBankResponse();
                foreach (var x in lines)
                {
                    var split = x.Split('\'');
                    var name = split[1];
                    var rate = Convert.ToDouble(split[3]);

                    response.currencies.Add(name, rate);
                }

                reader.Close();
                ecbData.Add(response);
                Logger.WriteLine("Updated Fiat Rates from European Central Bank");
            }
            catch (Exception e)
            {
                Logger.WriteLine(e.Message);
            }
        }

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
                Logger.WriteLine("Added Fiat rates to database from European Central Bank");
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
            var lastResponse = ecbData.LastOrDefault();
            if (lastResponse == null) return;
            var euroBase = lastResponse.currencies;


            rebasedCurrencies.Add("EUR", 1 / euroBase["CAD"]);

            foreach (var currency in euroBase)
            {
                var currencyName = currency.Key;
                var newValue = currency.Value / euroBase["CAD"];
                rebasedCurrencies.Add(currencyName, newValue);
            }
        }
    }
}