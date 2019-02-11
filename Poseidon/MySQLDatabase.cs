#region

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

#endregion

namespace Poseidon
{
    /// <summary>
    ///     Main class for interacting with MySQL Database
    /// </summary>
    public static class MySQLDatabase
    {
        /// <summary>
        ///     Holds a list of the table creation sql commands
        /// </summary>
        private static readonly List<string> tables = new List<string>();

        /// <summary>
        ///     Initialize the database.
        /// </summary>
        public static void Initialize()
        {
            CreateTables();
        }

        /// <summary>
        ///     Gets the connection string.
        /// </summary>
        public static MySqlConnection GetMySqlConnection()
        {
            var builder = new MySqlConnectionStringBuilder();
            builder["Server"] = Settings.GetDB_Host();
            builder["Database"] = Settings.GetDB_Name();
            builder["Port"] = Settings.GetDB_Port();
            builder["Uid"] = Settings.GetDB_Username();
            builder["Password"] = Settings.GetDB_Password();
            builder.SslMode = MySqlSslMode.Preferred;
            var connectionString = builder.ConnectionString;

            var connection = new MySqlConnection(connectionString);
            return connection;
        }

        /// <summary>
        ///     Creates the tables in the database.
        /// </summary>
        public static void CreateTables()
        {
            //Add tables that should be created here
            tables.Add(
                "CREATE TABLE Fiat_ECB (`Date` DATETIME NOT NULL,`EUR` DECIMAL(65,30) NOT NULL, `USD` DECIMAL(65,30) NOT NULL,`JPY` DECIMAL(65,30) NOT NULL,`BGN` DECIMAL(65,30) NOT NULL,`CZK` DECIMAL(65,30) NOT NULL,`DKK` DECIMAL(65,30) NOT NULL,`GBP` DECIMAL(65,30) NOT NULL,`HUF` DECIMAL(65,30) NOT NULL,`PLN` DECIMAL(65,30) NOT NULL,`RON` DECIMAL(65,30) NOT NULL,`SEK` DECIMAL(65,30) NOT NULL,`CHF` DECIMAL(65,30) NOT NULL,`ISK` DECIMAL(65,30) NOT NULL,`NOK` DECIMAL(65,30) NOT NULL,`HRK` DECIMAL(65,30) NOT NULL,`RUB` DECIMAL(65,30) NOT NULL,`TRY` DECIMAL(65,30) NOT NULL,`AUD` DECIMAL(65,30) NOT NULL,`BRL` DECIMAL(65,30) NOT NULL,`CAD` DECIMAL(65,30) NOT NULL,`CNY` DECIMAL(65,30) NOT NULL,`HKD` DECIMAL(65,30) NOT NULL,`IDR` DECIMAL(65,30) NOT NULL,`ILS` DECIMAL(65,30) NOT NULL,`INR` DECIMAL(65,30) NOT NULL,`KRW` DECIMAL(65,30) NOT NULL,`MXN` DECIMAL(65,30) NOT NULL,`MYR` DECIMAL(65,30) NOT NULL,`NZD` DECIMAL(65,30) NOT NULL,`PHP` DECIMAL(65,30) NOT NULL,`SGD` DECIMAL(65,30) NOT NULL,`THB` DECIMAL(65,30) NOT NULL,`ZAR` DECIMAL(65,30) NOT NULL,PRIMARY KEY (`Date`));");

            var conn = GetMySqlConnection();
            conn.Open();
            foreach (var query in tables)
            {
                var cmd = new MySqlCommand(query, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    Logger.WriteLine("Initialized a database");
                }
                catch (Exception e)
                {
                    Logger.WriteLine("Database Message: " + e.Message);
                }
            }

            conn.Close();
        }

        /// <summary>
        ///     Deletes all tables from database
        /// </summary>
        private static void DeleteTables()
        {
            var tablesToDrop = new List<string>();
            tablesToDrop.Add("Fiat_ECB");
            var conn = GetMySqlConnection();
            conn.Open();
            foreach (var table in tablesToDrop)
            {
                var commandText = "DROP TABLE if EXISTS " + table;
                var cmd = new MySqlCommand(commandText, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    Logger.WriteLine("Dropped a table");
                }
                catch (Exception e)
                {
                    Logger.WriteLine("Database Message: " + e.Message);
                }
            }

            conn.Close();
        }
    }
}