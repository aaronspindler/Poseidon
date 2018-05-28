using System;
using System.IO;
using System.Net;

namespace Poseidon
{
    public class Program
    {
        // State of the network connection
        private static bool NETWORK = false;
        // Kraken key
        private static string KEY;
        // Kraken signature
        private static string SIGNATURE;
        // Kraken Object
        private static Kraken kraken;
        // Fiat Object
        private static FiatCurrency fiat;
        // Database Object
        private static Database database;


        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        private static void Main()
        {
            Console.Title = "Poseidon";

            NETWORK = Utilities.CheckNetworkConnection();
            if (!NETWORK)
            {
                Console.WriteLine("No network connection!");
                Utilities.ExitProgram();
            }

            Settings.CheckSettingsFile();
            Settings.LoadSettings();

            database = new Database();
            database.CreateTables();

            kraken = new Kraken();
            Console.WriteLine(kraken.GetServerTime().result.rfc1123);

            fiat = new FiatCurrency();
            fiat.GetEcbData();


            var balances = kraken.GetAccountBalance().balances;
            Console.WriteLine(balances.ToStringTable(new[] { "Currency", "Amount" }, a => a.Key, a => a.Value));

            Console.ReadLine();
        }

        /// <summary>
        /// Gets the kraken.
        /// </summary>
        /// <returns>The kraken.</returns>
        public static Kraken GetKraken()
        {
            return kraken;
        }

        /// <summary>
        /// Gets the fiat currency.
        /// </summary>
        /// <returns>The fiat currency.</returns>
		public static FiatCurrency GetFiatCurrency()
        {
            return fiat;
        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <returns>The database.</returns>
		public static Database GetDatabase()
        {
            return database;
        }
    }
}