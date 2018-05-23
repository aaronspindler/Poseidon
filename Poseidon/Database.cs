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
		//public MySqlConnection GetMySqlConnection(){
		//	MySqlConnection connection;
		//	string connectionString;
		//	string server;
		//	string database;
		//	string uid;
		//	string password;
		//	string port;

		//	connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "U`enter code here`ID=" + uid + ";" + "PASSWORD=" + password + ";";

		//	connection = new MySqlConnection(connectionString);
		//	return connection;
		//}

        /// <summary>
        /// Creates the tables in the database.
        /// </summary>
		public void CreateTables(){
			//Add tables that should be created here
			tables.Add("CREATE TABLE `xnovax_poseidon`.`Fiat_ECB` (`Date` VARCHAR(12) NOT NULL,`USD` DECIMAL(65,30) NOT NULL,`JPY` DECIMAL(65,30) NOT NULL,`BGN` DECIMAL(65,30) NOT NULL,`CZK` DECIMAL(65,30) NOT NULL,`DKK` DECIMAL(65,30) NOT NULL,`GBP` DECIMAL(65,30) NOT NULL,`HUF` DECIMAL(65,30) NOT NULL,`PLN` DECIMAL(65,30) NOT NULL,`RON` DECIMAL(65,30) NOT NULL,`SEK` DECIMAL(65,30) NOT NULL,`CHF` DECIMAL(65,30) NOT NULL,`ISK` DECIMAL(65,30) NOT NULL,`NOK` DECIMAL(65,30) NOT NULL,`HRK` DECIMAL(65,30) NOT NULL,`RUB` DECIMAL(65,30) NOT NULL,`TRY` DECIMAL(65,30) NOT NULL,`AUD` DECIMAL(65,30) NOT NULL,`BRL` DECIMAL(65,30) NOT NULL,`CAD` DECIMAL(65,30) NOT NULL,`CNY` DECIMAL(65,30) NOT NULL,`HKD` DECIMAL(65,30) NOT NULL,`IDR` DECIMAL(65,30) NOT NULL,`ILS` DECIMAL(65,30) NOT NULL,`INR` DECIMAL(65,30) NOT NULL,`KRW` DECIMAL(65,30) NOT NULL,`MXN` DECIMAL(65,30) NOT NULL,`MYR` DECIMAL(65,30) NOT NULL,`NZD` DECIMAL(65,30) NOT NULL,`PHP` DECIMAL(65,30) NOT NULL,`SGD` DECIMAL(65,30) NOT NULL,`THB` DECIMAL(65,30) NOT NULL,`ZAR` DECIMAL(65,30) NOT NULL,PRIMARY KEY (`Date`));");


			foreach(string query in tables){
				
			}
		}
    }
}
