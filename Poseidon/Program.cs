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
        /// <summary>
        ///     Amount of milliseconds in 1 day
        /// </summary>
        public static int WAIT_ONE_DAY = 86400000; // 1 Day

        /// <summary>
        /// Amount of milliseconds in 1 hour
        /// </summary>
        public static int WAIT_ONE_HOUR = 1000;

        /// <summary>
        ///     Sleep time for the crypto data collection thread
        /// </summary>
        public static int CRYPTO_DATA_COLLECTION_RATE = 5;

        /// <summary>
        /// Sleep time for the network polling thread
        /// </summary>
        public static int NETWORK_POLL_RATE = 2500;

        // State of the network connection
        private static bool NETWORK;

        // Fiat Currency Objects
        private static EuropeanCentralBankManager ecbManager;
        private static BankOfCanadaManager bocManager;
        private static FixerManager fixManager;
        private static FiatCurrencyManager fiat;


        // Crypto Currency Objects
        private static Kraken kraken;
        private static CryptoCurrencyManager crypto;

        // Threads
        private static Thread ecbThread;
        private static Thread bocThread;
        private static Thread fixThread;
        private static Thread cryptoThread;
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

            ecbManager = new EuropeanCentralBankManager();
            bocManager = new BankOfCanadaManager();
            fixManager = new FixerManager();
            fiat = new FiatCurrencyManager(ecbManager,bocManager,fixManager);

            kraken = new Kraken();
            crypto = new CryptoCurrencyManager(kraken);

            Logger.WriteLine(kraken.GetServerTime().result.rfc1123);
            var balances = kraken.GetAccountBalance().balances;
            Logger.WriteLineNoDate(balances.ToStringTable(new[] {"Currency", "Amount"}, a => a.Key, a => a.Value));

            MySQLDatabase.Initialize();
            
            ecbThread = new Thread(UpdateECBData);
            ecbThread.Start();
            bocThread = new Thread(UpdateBOCData);
            bocThread.Start();
            fixThread = new Thread(UpdateFIXData);
            fixThread.Start();

            cryptoThread = new Thread(UpdateCryptoData);
            cryptoThread.Start();

            networkThread = new Thread(UpdateNetworkStatus);
            networkThread.Start();

            // Sleep main thread for 2000 milliseconds to allow data collection threads to get data
            Thread.Sleep(2000);
            
        }

        /// <summary>
        /// Updates European Central Bank Data
        /// </summary>
        private static void UpdateECBData()
        {
            while (true)
            {
                ecbManager.GetFiatRates();
                Thread.Sleep(WAIT_ONE_DAY);
            }
        }

        /// <summary>
        /// Updates Bank of Canada Data
        /// </summary>
        private static void UpdateBOCData()
        {
            while (true)
            {
                bocManager.GetFiatRates();
                Thread.Sleep(WAIT_ONE_DAY);
            }
        }

        /// <summary>
        /// Updates FIXER API Data
        /// </summary>
        private static void UpdateFIXData()
        {
            while (true)
            {
                fixManager.GetFiatRates();
                Thread.Sleep(WAIT_ONE_HOUR);
            }
        }

        /// <summary>
        ///     Updates the crypto data.
        /// </summary>
        private static void UpdateCryptoData()
        {
            while (true)
                //crypto.GetKrakenData();
                Thread.Sleep(CRYPTO_DATA_COLLECTION_RATE);
        }

        /// <summary>
        /// Checks the network to see if there is a connection
        /// </summary>
        private static void UpdateNetworkStatus()
        {
            while (true)
            {
                if (!Utilities.CheckNetworkConnection())
                {
                    Logger.WriteLine("Network connection disconnected");
                    Utilities.ExitProgram();
                }

                Thread.Sleep(NETWORK_POLL_RATE);
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