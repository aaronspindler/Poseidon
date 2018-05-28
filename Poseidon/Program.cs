// --- Program.cs ---
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