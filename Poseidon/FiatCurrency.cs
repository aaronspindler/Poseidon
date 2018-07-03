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
using System.Xml;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using Poseidon.Models.FiatCurrency;

namespace Poseidon
{
    public class FiatCurrency
    {
		List<EuropeanCentralBankResponse> ecbData;

		/// <summary>
        /// Initializes a new instance of the <see cref="T:Poseidon.FiatCurrency"/> class.
        /// </summary>
        public FiatCurrency()
        {
			ecbData = new List<EuropeanCentralBankResponse>();
        }

        /// <summary>
		/// Gets current data containing exchange rates for fiat currencies from the ECB (European Central Bank)
        /// </summary>
        public void GetEcbData()
        {
			System.IO.Directory.CreateDirectory("Fiat/ECB");
            var now = DateTime.Now.Ticks;
			var xmlDataFileName = string.Format(@"Fiat/ECB/{0}.txt", now);
            try
			{
				WebClient client = new WebClient();
				Stream data = client.OpenRead("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
				StreamReader reader = new StreamReader(data);
				StreamWriter writer = new StreamWriter(xmlDataFileName);
                //Read the header and ignore it
				for (int i = 0; i < 7; i++){
					reader.ReadLine();
				}

                ///Write the date
				writer.WriteLine(reader.ReadLine().Trim());

                ///Read and write the actual currency data
				for (int i = 0; i < 32; i++){
					writer.WriteLine(reader.ReadLine().Trim());
				}

				///Close reader and writers
				writer.Close();
				reader.Close();

				reader = new StreamReader(xmlDataFileName);

                ///Skip over the date line
				String date = reader.ReadLine();
                String[] dateSplit = date.Split('\'');
                date = dateSplit[1];

                //Start the loop
                EuropeanCentralBankResponse response = new EuropeanCentralBankResponse();
				string txt = reader.ReadLine();
				while(txt != null){
					string[] split = txt.Split('\'');
					string name = split[1];
					double rate = Convert.ToDouble(split[3]);

					response.currencies.Add(name, rate);
					txt = reader.ReadLine();
				}
				reader.Close();
                ecbData.Add(response);

                //Database Data Storage
                MySqlConnection conn = Database.GetMySqlConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Fiat_ECB(Date, USD, JPY, BGN, CZK, DKK, GBP, HUF, PLN, RON, SEK, CHF, ISK, NOK, HRK, RUB, TRY, AUD, BRL, CAD, CNY, HKD, IDR, ILS, INR, KRW, MXN, MYR, NZD, PHP, SGD, THB, ZAR) VALUES(@Date, @USD, @JPY, @BGN, @CZK, @DKK, @GBP, @HUF, @PLN, @RON, @SEK, @CHF, @ISK, @NOK, @HRK, @RUB, @TRY, @AUD, @BRL, @CAD, @CNY, @HKD, @IDR, @ILS, @INR, @KRW, @MXN, @MYR, @NZD, @PHP, @SGD, @THB, @ZAR)";
                cmd.Parameters.Add("@Date", date);
                cmd.Parameters.Add("@USD", response.currencies["USD"]);
                cmd.Parameters.Add("@JPY", response.currencies["JPY"]);
                cmd.Parameters.Add("@BGN", response.currencies["BGN"]);
                cmd.Parameters.Add("@CZK", response.currencies["CZK"]);
                cmd.Parameters.Add("@DKK", response.currencies["DKK"]);
                cmd.Parameters.Add("@GBP", response.currencies["GBP"]);
                cmd.Parameters.Add("@HUF", response.currencies["HUF"]);
                cmd.Parameters.Add("@PLN", response.currencies["PLN"]);
                cmd.Parameters.Add("@RON", response.currencies["RON"]);
                cmd.Parameters.Add("@SEK", response.currencies["SEK"]);
                cmd.Parameters.Add("@CHF", response.currencies["CHF"]);
                cmd.Parameters.Add("@ISK", response.currencies["ISK"]);
                cmd.Parameters.Add("@NOK", response.currencies["NOK"]);
                cmd.Parameters.Add("@HRK", response.currencies["HRK"]);
                cmd.Parameters.Add("@RUB", response.currencies["RUB"]);
                cmd.Parameters.Add("@TRY", response.currencies["TRY"]);
                cmd.Parameters.Add("@AUD", response.currencies["AUD"]);
                cmd.Parameters.Add("@BRL", response.currencies["BRL"]);
                cmd.Parameters.Add("@CAD", response.currencies["CAD"]);
                cmd.Parameters.Add("@CNY", response.currencies["CNY"]);
                cmd.Parameters.Add("@HKD", response.currencies["HKD"]);
                cmd.Parameters.Add("@IDR", response.currencies["IDR"]);
                cmd.Parameters.Add("@ILS", response.currencies["ILS"]);
                cmd.Parameters.Add("@INR", response.currencies["INR"]);
                cmd.Parameters.Add("@KRW", response.currencies["KRW"]);
                cmd.Parameters.Add("@MXN", response.currencies["MXN"]);
                cmd.Parameters.Add("@MYR", response.currencies["MYR"]);
                cmd.Parameters.Add("@NZD", response.currencies["NZD"]);
                cmd.Parameters.Add("@PHP", response.currencies["PHP"]);
                cmd.Parameters.Add("@SGD", response.currencies["SGD"]);
                cmd.Parameters.Add("@THB", response.currencies["THB"]);
                cmd.Parameters.Add("@ZAR", response.currencies["ZAR"]);
                cmd.ExecuteNonQuery();
                conn.Close();

            }catch(Exception e){
                Logger.WriteLine(e.Message);
            }
        }
    }
}
