#region

using System;
using System.Linq;
using System.Threading;
using Poseidon.Crypto;
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
        private static KrakenManager krakenManager;

        // Threads
        private static Thread cryptoThread;
        private static Thread networkThread;

        /// <summary>
        ///     The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">Startup arguments</param>
        private static void Main(string[] args)
        {
            StartUp(args);
            Body();
            Testing();
        }

        /// <summary>
        ///     Code that is ran on startup to initialize everything
        /// </summary>
        /// <param name="args"></param>
        private static void StartUp(string[] args)
        {
            Console.Title = "Poseidon";

            Logger.Startup();

            CheckArgs(args);

            Logger.WriteLine("Welcome to Poseidon!");

            NETWORK = Utilities.CheckNetworkConnection();
            if (!NETWORK)
            {
                Logger.WriteLine("No network connection!");
                Utilities.ExitProgram(true);
            }

            Settings.Startup(args);
            Database.Startup();

            ecbManager = new EuropeanCentralBankManager();
            bocManager = new BankOfCanadaManager();
            fixManager = new FixerManager();
            fiat = new FiatManager(ecbManager, bocManager, fixManager);


            krakenManager = new KrakenManager();

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
            Logger.WriteLine(krakenManager.GetServerTimeFormatted());
            var balances = krakenManager.GetAccountBalance();
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


        //TODO: Fully implement a full spread of special cases
        /// <summary>
        ///     Checks for special cases in the arguments
        ///     help - display the help information for the program
        /// </summary>
        /// <param name="args">Arguments passed by the command line</param>
        private static void CheckArgs(string[] args)
        {
            if (args.Contains("help"))
            {
                Logger.WriteLineNoDate("=================================================================");
                Logger.WriteLineNoDate("Poseidon Help");
                Logger.WriteLineNoDate("Settings file must be fully filled out for the program to function");
                Logger.WriteLineNoDate("=================================================================");

                Utilities.ExitProgram(false);
            }
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
                    Utilities.ExitProgram(true);
                }

                Thread.Sleep(Globals.NETWORK_POLL_RATE);
            }
        }
    }
}