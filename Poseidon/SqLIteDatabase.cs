using System;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;

namespace Poseidon
{
    /// <summary>
    /// Database Class
    /// </summary>
    public static class SqLiteDatabase
    {
        private static SqliteConnection dbConnection;

        /// <summary>
        /// Database Constructor
        /// </summary>
        static SqLiteDatabase()
        {
            Create();
            dbConnection = new SqliteConnection("Data Source=Database.sqlite;");
            InitializeTables();
        }

        static void Insert()
        {
            
        }

        /// <summary>
        /// Create the database
        /// </summary>
        public static void Create()
        {
            SQLiteConnection.CreateFile("Database.sqlite");
        }

        public static void InitializeTables()
        {
            dbConnection.Open();
            string sql;
            sql = "CREATE TABLE Fiat_ECB (`Date` DATETIME NOT NULL,`EUR` DECIMAL(65,30) NOT NULL, `USD` DECIMAL(65,30) NOT NULL,`JPY` DECIMAL(65,30) NOT NULL,`BGN` DECIMAL(65,30) NOT NULL,`CZK` DECIMAL(65,30) NOT NULL,`DKK` DECIMAL(65,30) NOT NULL,`GBP` DECIMAL(65,30) NOT NULL,`HUF` DECIMAL(65,30) NOT NULL,`PLN` DECIMAL(65,30) NOT NULL,`RON` DECIMAL(65,30) NOT NULL,`SEK` DECIMAL(65,30) NOT NULL,`CHF` DECIMAL(65,30) NOT NULL,`ISK` DECIMAL(65,30) NOT NULL,`NOK` DECIMAL(65,30) NOT NULL,`HRK` DECIMAL(65,30) NOT NULL,`RUB` DECIMAL(65,30) NOT NULL,`TRY` DECIMAL(65,30) NOT NULL,`AUD` DECIMAL(65,30) NOT NULL,`BRL` DECIMAL(65,30) NOT NULL,`CAD` DECIMAL(65,30) NOT NULL,`CNY` DECIMAL(65,30) NOT NULL,`HKD` DECIMAL(65,30) NOT NULL,`IDR` DECIMAL(65,30) NOT NULL,`ILS` DECIMAL(65,30) NOT NULL,`INR` DECIMAL(65,30) NOT NULL,`KRW` DECIMAL(65,30) NOT NULL,`MXN` DECIMAL(65,30) NOT NULL,`MYR` DECIMAL(65,30) NOT NULL,`NZD` DECIMAL(65,30) NOT NULL,`PHP` DECIMAL(65,30) NOT NULL,`SGD` DECIMAL(65,30) NOT NULL,`THB` DECIMAL(65,30) NOT NULL,`ZAR` DECIMAL(65,30) NOT NULL,PRIMARY KEY (`Date`));";
            SqliteCommand command = new SqliteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
            sql = "CREATE TABLE Fiat_BOC (`Date` DATETIME NOT NULL,`AUD` DECIMAL(65,30) NOT NULL,`BRL` DECIMAL(65,30) NOT NULL,`CHF` DECIMAL(65,30) NOT NULL,`CNY` DECIMAL(65,30) NOT NULL,`EUR` DECIMAL(65,30) NOT NULL,`GBP` DECIMAL(65,30) NOT NULL,`HKD` DECIMAL(65,30) NOT NULL,`IDR` DECIMAL(65,30) NOT NULL,`INR` DECIMAL(65,30) NOT NULL,`JPY` DECIMAL(65,30) NOT NULL,`KRW` DECIMAL(65,30) NOT NULL,`MXN` DECIMAL(65,30) NOT NULL,`MYR` DECIMAL(65,30) NOT NULL,`NOK` DECIMAL(65,30) NOT NULL,`NZD` DECIMAL(65,30) NOT NULL,`PEN` DECIMAL(65,30) NOT NULL,`RUB` DECIMAL(65,30) NOT NULL,`SAR` DECIMAL(65,30) NOT NULL,`SEK` DECIMAL(65,30) NOT NULL,`SGD` DECIMAL(65,30) NOT NULL,`THB` DECIMAL(65,30) NOT NULL,`TRY` DECIMAL(65,30) NOT NULL,`TWD` DECIMAL(65,30) NOT NULL,`USD` DECIMAL(65,30) NOT NULL,`VND` DECIMAL(65,30) NOT NULL,`ZAR` DECIMAL(65,30) NOT NULL,PRIMARY KEY (`Date`));";
            command = new SqliteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
            dbConnection.Close();
            Console.WriteLine("SqLiteCreated");
        }
    }
}