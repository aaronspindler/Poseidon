#region

using System;
using System.Threading;
using Poseidon.Crypto;
using Poseidon.Database;
using Poseidon.Fiat;
using Poseidon.Misc;

#endregion

namespace Poseidon
{
    /// <summary>
    ///     The main program class
    /// </summary>
    public class Program
    {
        // State of the network connection
        private static bool NETWORK;

        // Fiat Currency Objects
        private static EuropeanCentralBankManager ecbManager;
        private static BankOfCanadaManager bocManager;
        private static FixerManager fixManager;
        private static FiatManager fiat;


        // Crypto Currency Objects
        private static Kraken kraken;
        private static CryptoCurrencyManager crypto;

        // Threads
        private static Thread cryptoThread;
        private static Thread networkThread;

        /// <summary>
        ///     The entry point of the program, where the program control starts and ends.
        /// </summary>
        private static void Main()
        {
            StartUp();
            Body();
            Testing();
        }

        /// <summary>
        ///     Code that is ran on startup to initialize everything
        /// </summary>
        private static void StartUp()
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
            fiat = new FiatManager(ecbManager, bocManager, fixManager);

            kraken = new Kraken();
            crypto = new CryptoCurrencyManager(kraken);

            MySQLDatabase.Initialize();

            cryptoThread = new Thread(UpdateCryptoData);
            cryptoThread.Start();

            networkThread = new Thread(UpdateNetworkStatus);
            networkThread.Start();

            fiat.StartThreads();

            // Sleep main thread for 2000 milliseconds to allow data collection threads to get data
            Thread.Sleep(2000);
        }

        /// <summary>
        ///     Main working body of the program
        /// </summary>
        public static void Body()
        {
            Logger.WriteLine(kraken.GetServerTime().result.rfc1123);
            var balances = kraken.GetAccountBalance().balances;
            Logger.WriteLineNoDate(balances.ToStringTable(new[] {"Currency", "Amount"}, a => a.Key, a => a.Value));
        }


        /// <summary>
        ///     Code that is being tested / implemented
        ///     Will be moved to body upon complete testing
        /// </summary>
        public static void Testing()
        {
            try
            {
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        /// <summary>
        ///     Updates the crypto data.
        /// </summary>
        private static void UpdateCryptoData()
        {
            while (true)
                //crypto.GetKrakenData();
                Thread.Sleep(Globals.CRYPTO_DATA_COLLECTION_RATE);
        }

        /// <summary>
        ///     Checks the network to see if there is a connection
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

                Thread.Sleep(Globals.NETWORK_POLL_RATE);
            }
        }
    }
}