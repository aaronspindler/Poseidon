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
using System.Threading;

namespace Poseidon
{
    public class Program
    {
        // Time to wait inbetween polling for data in milliseconds
        public static int DATA_COLLECTION_RATE = 10000;
        // State of the network connection
        private static bool NETWORK = false;
        // Kraken Object
        private static Kraken kraken;
        // Fiat Object
        private static FiatCurrency fiat;

        // Threads
        private static Thread fiatThread;
        private static Thread cryptoThread;
        private static Thread dataThread;


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

            kraken = new Kraken();
            Console.WriteLine(kraken.GetServerTime().result.rfc1123);
            var balances = kraken.GetAccountBalance().balances;
            Console.WriteLine(balances.ToStringTable(new[] { "Currency", "Amount" }, a => a.Key, a => a.Value));

            Database.Initialize();

            fiat = new FiatCurrency();
            fiatThread = new Thread(UpdateFiatData);
            fiatThread.Start();



            Console.ReadLine();
        }

        public static void MenuScreen(){
            Console.WriteLine("");
        }

        /// <summary>
        /// Updates the fiat data.
        /// </summary>
        private static void UpdateFiatData(){
            while (true)
            {
                fiat.GetEcbData();
                Thread.Sleep(DATA_COLLECTION_RATE);
            }
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
    }
}