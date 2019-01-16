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
using System.Net;
using Poseidon.Models.FiatCurrency;

namespace Poseidon
{
    public class FiatCurrency
    {
        private readonly List<EuropeanCentralBankResponse> ecbData;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Poseidon.FiatCurrency" /> class.
        /// </summary>
        public FiatCurrency()
        {
            ecbData = new List<EuropeanCentralBankResponse>();
        }

        /// <summary>
        ///     Gets current data containing exchange rates for fiat currencies from the ECB (European Central Bank)
        /// </summary>
        public void GetEcbData()
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

                ///Write the date
                writer.WriteLine(reader.ReadLine().Trim());

                ///Read and write the actual currency data
                for (var i = 0; i < 32; i++) writer.WriteLine(reader.ReadLine().Trim());

                ///Close reader and writers
                writer.Close();
                reader.Close();

                reader = new StreamReader(xmlDataFileName);

                ///Skip over the date line
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

                //Database Data Storage
                var conn = Database.GetMySqlConnection();
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText =
                    "INSERT INTO Fiat_ECB(Date, USD, JPY, BGN, CZK, DKK, GBP, HUF, PLN, RON, SEK, CHF, ISK, NOK, HRK, RUB, TRY, AUD, BRL, CAD, CNY, HKD, IDR, ILS, INR, KRW, MXN, MYR, NZD, PHP, SGD, THB, ZAR) VALUES(@Date, @USD, @JPY, @BGN, @CZK, @DKK, @GBP, @HUF, @PLN, @RON, @SEK, @CHF, @ISK, @NOK, @HRK, @RUB, @TRY, @AUD, @BRL, @CAD, @CNY, @HKD, @IDR, @ILS, @INR, @KRW, @MXN, @MYR, @NZD, @PHP, @SGD, @THB, @ZAR)";
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@USD", response.currencies["USD"]);
                cmd.Parameters.AddWithValue("@JPY", response.currencies["JPY"]);
                cmd.Parameters.AddWithValue("@BGN", response.currencies["BGN"]);
                cmd.Parameters.AddWithValue("@CZK", response.currencies["CZK"]);
                cmd.Parameters.AddWithValue("@DKK", response.currencies["DKK"]);
                cmd.Parameters.AddWithValue("@GBP", response.currencies["GBP"]);
                cmd.Parameters.AddWithValue("@HUF", response.currencies["HUF"]);
                cmd.Parameters.AddWithValue("@PLN", response.currencies["PLN"]);
                cmd.Parameters.AddWithValue("@RON", response.currencies["RON"]);
                cmd.Parameters.AddWithValue("@SEK", response.currencies["SEK"]);
                cmd.Parameters.AddWithValue("@CHF", response.currencies["CHF"]);
                cmd.Parameters.AddWithValue("@ISK", response.currencies["ISK"]);
                cmd.Parameters.AddWithValue("@NOK", response.currencies["NOK"]);
                cmd.Parameters.AddWithValue("@HRK", response.currencies["HRK"]);
                cmd.Parameters.AddWithValue("@RUB", response.currencies["RUB"]);
                cmd.Parameters.AddWithValue("@TRY", response.currencies["TRY"]);
                cmd.Parameters.AddWithValue("@AUD", response.currencies["AUD"]);
                cmd.Parameters.AddWithValue("@BRL", response.currencies["BRL"]);
                cmd.Parameters.AddWithValue("@CAD", response.currencies["CAD"]);
                cmd.Parameters.AddWithValue("@CNY", response.currencies["CNY"]);
                cmd.Parameters.AddWithValue("@HKD", response.currencies["HKD"]);
                cmd.Parameters.AddWithValue("@IDR", response.currencies["IDR"]);
                cmd.Parameters.AddWithValue("@ILS", response.currencies["ILS"]);
                cmd.Parameters.AddWithValue("@INR", response.currencies["INR"]);
                cmd.Parameters.AddWithValue("@KRW", response.currencies["KRW"]);
                cmd.Parameters.AddWithValue("@MXN", response.currencies["MXN"]);
                cmd.Parameters.AddWithValue("@MYR", response.currencies["MYR"]);
                cmd.Parameters.AddWithValue("@NZD", response.currencies["NZD"]);
                cmd.Parameters.AddWithValue("@PHP", response.currencies["PHP"]);
                cmd.Parameters.AddWithValue("@SGD", response.currencies["SGD"]);
                cmd.Parameters.AddWithValue("@THB", response.currencies["THB"]);
                cmd.Parameters.AddWithValue("@ZAR", response.currencies["ZAR"]);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Logger.WriteLine(e.Message);
            }
        }
    }
}