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
			var xmlDataFileName = string.Format(@"Fiat/ECB/{0}.txt", DateTime.Now.Ticks);
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
				reader.ReadLine();

                //Start the loop
                EuropeanCentralBankResponse response = new EuropeanCentralBankResponse();
				string txt = reader.ReadLine();
				while(txt != null){
					string[] split = txt.Split('\'');
					string name = split[1];
					double rate = Convert.ToDouble(split[3]);

					response.currencies.Add(new Currency(name, rate));
					txt = reader.ReadLine();
				}
				reader.Close();
                ecbData.Add(response);

                MySqlConnection conn = Database.GetMySqlConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Fiat_ECB(Date, USD, JPY, BGN, CZK, DKK, GBP, HUF, PLN, RON, SEK, CHF, ISK, NOK, HRK, RUB, TRY, AUD, BRL, CAD, CNY, HKD, IDR, ILS, INR, KRW, MXN, MYR, NZD, PHP, SGD, THB, ZAR) VALUES(@Date, @USD, @JPY, @BGN, @CZK, @DKK, @GBP, @HUF, @PLN, @RON, @SEK, @CHF, @ISK, @NOK, @HRK, @RUB, @TRY, @AUD, @BRL, @CAD, @CNY, @HKD, @IDR, @ILS, @INR, @KRW, @MXN, @MYR, @NZD, @PHP, @SGD, @THB, @ZAR)";
                cmd.Parameters.Add("@Date","asdf");
                cmd.Parameters.Add("@USD", "1.1");
                cmd.Parameters.Add("@JPY", "1.1");
                cmd.Parameters.Add("@BGN", "1.1");
                cmd.Parameters.Add("@CZK", "1.1");
                cmd.Parameters.Add("@DKK", "1.1");
                cmd.Parameters.Add("@GBP", "1.1");
                cmd.Parameters.Add("@HUF", "1.1");
                cmd.Parameters.Add("@PLN", "1.1");
                cmd.Parameters.Add("@RON", "1.1");
                cmd.Parameters.Add("@SEK", "1.1");
                cmd.Parameters.Add("@CHF", "1.1");
                cmd.Parameters.Add("@ISK", "1.1");
                cmd.Parameters.Add("@NOK", "1.1");
                cmd.Parameters.Add("@HRK", "1.1");
                cmd.Parameters.Add("@RUB", "1.1");
                cmd.Parameters.Add("@TRY", "1.1");
                cmd.Parameters.Add("@AUD", "1.1");
                cmd.Parameters.Add("@BRL", "1.1");
                cmd.Parameters.Add("@CAD", "1.1");
                cmd.Parameters.Add("@CNY", "1.1");
                cmd.Parameters.Add("@HKD", "1.1");
                cmd.Parameters.Add("@IDR", "1.1");
                cmd.Parameters.Add("@ILS", "1.1");
                cmd.Parameters.Add("@INR", "1.1");
                cmd.Parameters.Add("@KRW", "1.1");
                cmd.Parameters.Add("@MXN", "1.1");
                cmd.Parameters.Add("@MYR", "1.1");
                cmd.Parameters.Add("@NZD", "1.1");
                cmd.Parameters.Add("@PHP", "1.1");
                cmd.Parameters.Add("@SGD", "1.1");
                cmd.Parameters.Add("@THB", "1.1");
                cmd.Parameters.Add("@ZAR", "1.1");
                cmd.ExecuteNonQuery();
                conn.Close();



            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}
