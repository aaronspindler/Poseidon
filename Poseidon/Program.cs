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
		// Settings Object
		private static Settings settings;

        
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

            CheckKrakenKeyFile();
            LoadKrakenKeys();

			settings = new Settings();
			settings.CheckSettingsFile();
			settings.LoadSettings();

			database = new Database();
			database.CreateTables();
            
            
            kraken = new Kraken(KEY, SIGNATURE);
            Console.WriteLine(kraken.GetServerTime().result.rfc1123);

            fiat = new FiatCurrency();
            fiat.GetEcbData();


            var balances = kraken.GetAccountBalance().balances;
            Console.WriteLine(balances.ToStringTable(new[] {"Currency", "Amount"}, a => a.Key, a => a.Value));

            Console.ReadLine();
        }
        /// <summary>
        /// Checks the key file.
        /// </summary>
        public static void CheckKrakenKeyFile()
        {
            if (!File.Exists("KrakenAPI.txt"))
                try
                {
                    using (var sw = File.CreateText("KrakenAPI.txt"))
                    {
                        sw.WriteLine("KEY=");
                        sw.WriteLine("SIGNATURE=");
                    }
                    Console.WriteLine("Please enter your credentials in KrakenAPI.txt");

                    Utilities.ExitProgram();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);

                    Utilities.ExitProgram();
                }
        }
        /// <summary>
        /// Loads the keys.
        /// </summary>
        public static void LoadKrakenKeys()
        {
            using (var sr = new StreamReader("KrakenAPI.txt"))
            {
                var line = sr.ReadLine();
                if (line != null && line.Substring(0, 4) == "KEY=")
                    KEY = line.Substring(4);
                line = sr.ReadLine();
                if (line != null && line.Substring(0, 10) == "SIGNATURE=")
                    SIGNATURE = line.Substring(10);
            }
        }

        public static Kraken GetKraken()
        {
            return kraken;
        }
    }
}