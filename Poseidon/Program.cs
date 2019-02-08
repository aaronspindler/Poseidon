#region

using System;
using System.Threading;

#endregion

namespace Poseidon
{
    /// <summary>
    ///     The main program class
    /// </summary>
    public class Program
    {
        // Time to wait inbetween polling for data in milliseconds
        public static int FIAT_DATA_COLLECTION_RATE = 86400000; // 1 Day
        public static int CRYPTO_DATA_COLLECTION_RATE = 5;

        // State of the network connection
        private static bool NETWORK;

        // Kraken Object
        private static Kraken kraken;

        // Fiat Object
        private static FiatCurrencyManager fiat;

        // Crypto Object
        private static CryptoCurrencyManager crypto;

        // Threads
        private static Thread fiatThread;
        private static Thread cryptoThread;
        private static Thread dataThread;
        private static Thread networkThread;


        /// <summary>
        ///     The entry point of the program, where the program control starts and ends.
        /// </summary>
        private static void Main()
        {
            Console.Title = "Poseidon";

            Logger.Initialize();

            Logger.WriteLine("Welcome to Poseidon!");

            NETWORK = Utilities.CheckNetworkConnection();
            if (!NETWORK)
            {
                Logger.WriteLine("No network connection!");
                Utilities.ExitProgram();
            }

            Settings.CheckSettingsFile();
            Settings.LoadSettings();

            fiat = new FiatCurrencyManager();
            kraken = new Kraken();
            crypto = new CryptoCurrencyManager(kraken);

            Logger.WriteLine(kraken.GetServerTime().result.rfc1123);
            var balances = kraken.GetAccountBalance().balances;
            Logger.WriteLineNoDate(balances.ToStringTable(new[] {"Currency", "Amount"}, a => a.Key, a => a.Value));

            MySQLDatabase.Initialize();

            fiatThread = new Thread(UpdateFiatData);
            fiatThread.Start();

            cryptoThread = new Thread(UpdateCryptoData);
            cryptoThread.Start();

            networkThread = new Thread(UpdateNetworkStatus);
            networkThread.Start();

            // Sleep main thread for 500 milliseconds to allow data collection threads to get data
            Thread.Sleep(500);
        }

        /// <summary>
        ///     Updates the fiat data.
        /// </summary>
        private static void UpdateFiatData()
        {
            while (true)
            {
                fiat.GetFiatRates();
                Thread.Sleep(FIAT_DATA_COLLECTION_RATE);
            }
        }

        /// <summary>
        ///     Updates the crypto data.
        /// </summary>
        private static void UpdateCryptoData()
        {
            while (true)
            {
                crypto.GetKrakenData();
                Thread.Sleep(CRYPTO_DATA_COLLECTION_RATE);
            }
        }

        private static void UpdateNetworkStatus()
        {
            while (true)
            {
                if (!Utilities.CheckNetworkConnection())
                {
                    Logger.WriteLine("Network connection disconnected");
                    Utilities.ExitProgram();
                }

                Thread.Sleep(3000);
            }
        }

        /// <summary>
        ///     Gets the kraken.
        /// </summary>
        /// <returns>The kraken.</returns>
        public static Kraken GetKraken()
        {
            return kraken;
        }

        /// <summary>
        ///     Gets the fiat currency.
        /// </summary>
        /// <returns>The fiat currency.</returns>
        public static FiatCurrencyManager GetFiatCurrency()
        {
            return fiat;
        }

        /// <summary>
        ///     Gets the crypto currency.
        /// </summary>
        /// <returns>The crypto currency.</returns>
        public static CryptoCurrencyManager GetCryptoCurrency()
        {
            return crypto;
        }
    }
}