// --- Database.cs ---
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
using MySql.Data.MySqlClient;

namespace Poseidon
{
    public class Database
    {
		List<String> tables = new List<string>();
		/// <summary>
        /// Initializes a new instance of the <see cref="T:Poseidon.Database"/> class.
        /// </summary>
        public Database()
        {
			
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
		public MySqlConnection GetMySqlConnection(){

            var builder = new MySqlConnectionStringBuilder();
            builder["Server"] = Settings.GetDB_Host();
            builder["Database"] = Settings.GetDB_Name();
            builder["Port"] = Settings.GetDB_Port();
            builder["Uid"] = Settings.GetDB_Username();
            builder["Password"] = Settings.GetDB_Password();
            builder.SslMode = MySqlSslMode.Preferred;
            string connectionString = builder.ConnectionString;

			MySqlConnection connection = new MySqlConnection(connectionString);
			return connection;
		}

        /// <summary>
        /// Creates the tables in the database.
        /// </summary>
		public void CreateTables(){
			//Add tables that should be created here
			tables.Add("CREATE TABLE `xnovax_poseidon`.`Fiat_ECB` (`Date` VARCHAR(12) NOT NULL,`USD` DECIMAL(65,30) NOT NULL,`JPY` DECIMAL(65,30) NOT NULL,`BGN` DECIMAL(65,30) NOT NULL,`CZK` DECIMAL(65,30) NOT NULL,`DKK` DECIMAL(65,30) NOT NULL,`GBP` DECIMAL(65,30) NOT NULL,`HUF` DECIMAL(65,30) NOT NULL,`PLN` DECIMAL(65,30) NOT NULL,`RON` DECIMAL(65,30) NOT NULL,`SEK` DECIMAL(65,30) NOT NULL,`CHF` DECIMAL(65,30) NOT NULL,`ISK` DECIMAL(65,30) NOT NULL,`NOK` DECIMAL(65,30) NOT NULL,`HRK` DECIMAL(65,30) NOT NULL,`RUB` DECIMAL(65,30) NOT NULL,`TRY` DECIMAL(65,30) NOT NULL,`AUD` DECIMAL(65,30) NOT NULL,`BRL` DECIMAL(65,30) NOT NULL,`CAD` DECIMAL(65,30) NOT NULL,`CNY` DECIMAL(65,30) NOT NULL,`HKD` DECIMAL(65,30) NOT NULL,`IDR` DECIMAL(65,30) NOT NULL,`ILS` DECIMAL(65,30) NOT NULL,`INR` DECIMAL(65,30) NOT NULL,`KRW` DECIMAL(65,30) NOT NULL,`MXN` DECIMAL(65,30) NOT NULL,`MYR` DECIMAL(65,30) NOT NULL,`NZD` DECIMAL(65,30) NOT NULL,`PHP` DECIMAL(65,30) NOT NULL,`SGD` DECIMAL(65,30) NOT NULL,`THB` DECIMAL(65,30) NOT NULL,`ZAR` DECIMAL(65,30) NOT NULL,PRIMARY KEY (`Date`));");

            MySqlConnection conn = GetMySqlConnection();
            conn.Open();
			foreach(string query in tables){
                MySqlCommand cmd = new MySqlCommand(query, conn);
                try{
                    cmd.ExecuteNonQuery();
                }catch(Exception e){
                    Console.WriteLine("Database Message: " + e.Message);
                }
			}
            conn.Close();
		}
    }
}
