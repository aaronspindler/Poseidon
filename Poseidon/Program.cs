﻿using System;
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

            CheckNetworkConnection();
			if (!NETWORK)
            {
                Console.WriteLine("No network connection!");
                ExitProgram();
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

                    ExitProgram();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);

                    ExitProgram();
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

        /// <summary>
        /// Exits the program.
        /// </summary>
        public static void ExitProgram()
        {
            Console.WriteLine("Press enter to close application");
            Console.ReadLine();
            Environment.Exit(-1);
        }
        /// <summary>
        /// Writes to a file.
        /// </summary>
        /// <param name="text">The text thats written to the file</param>
        public static void WriteToFile(string text)
        {
            using (var sw = File.CreateText("temp.txt"))
            {
                sw.Write(text);
            }
            Console.WriteLine("File written");
        }
        /// <summary>
        /// Writes to a file.
        /// </summary>
        /// <param name="fileName">File name of where to write the text</param>
        /// <param name="text">The text thats written toa  file</param>
        public static void WriteToFile(string fileName, string text)
        {
            using (var sw = File.CreateText(fileName + "_" + DateTime.Now.ToFileTime() + ".txt"))
            {
                sw.Write(text);
            }
            Console.WriteLine("File written");
        }

        /// <summary>
        /// Gets the Unix time and puts it to a string.
        /// </summary>
        /// <returns>The time to string.</returns>
        /// <param name="unix">Unix.</param>
        public static string UnixTimeToString(decimal unix)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds((double) unix).ToLocalTime();
            return dtDateTime.ToString();
        }

        /// <summary>
        ///     Checks for an available internet connection by pinging google.com
        /// </summary>
        public static void CheckNetworkConnection()
        {
            try
			{
                using (var client = new WebClient())
                using (Stream stream = client.OpenRead("http://www.google.com"))
                {
                    NETWORK = true;
                }
            }
            catch
            {
                NETWORK = false;
            }
        }

        public static Kraken GetKraken()
        {
            return kraken;
        }
    }
}