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
        
        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        private static void Main()
        {
            Console.Title = "Poseidon";
            CheckNetworkConnection();
            CheckKeyFile();
            LoadKeys();
            //CheckSettingsFile();
            //LoadSettings();

            if (!NETWORK)
            {
                Console.WriteLine("No network connection!");
                ExitProgram();
            }
            
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
        public static void CheckKeyFile()
        {
            if (!File.Exists("API.txt"))
                try
                {
                    using (var sw = File.CreateText("API.txt"))
                    {
                        sw.WriteLine("KEY=");
                        sw.WriteLine("SIGNATURE=");
                    }
                    Console.WriteLine("Please enter your credentials in API.txt");

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
        public static void LoadKeys()
        {
            using (var sr = new StreamReader("API.txt"))
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
        /// Checks the settings file.
        /// </summary>
        public static void CheckSettingsFile()
        {
            if (!File.Exists("settings.txt"))
            {
               CreateDefaultSettingsFile();
            }
            else
            {
                using (var sr = new StreamReader("settings.txt"))
                {
                    var line = sr.ReadLine();
                    if (line.Substring(line.LastIndexOf('='), (line.Length - line.LastIndexOf('='))) !=
                        System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
                    {
						Console.WriteLine("Looks like you were using a previous version of the settings file.");
						CreateDefaultSettingsFile();
                        Console.WriteLine("Your settings file has been remade");
                    }
                }
            }
        }
        /// <summary>
        /// Creates a default settings file.
        /// </summary>
        public static void CreateDefaultSettingsFile()
        {
            try
            {
                using (var sw = File.CreateText("settings.txt"))
                {
                    sw.WriteLine("VERSION=" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
                    sw.WriteLine("CURRENCY=CAD");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

                ExitProgram();
            }
        }
        /// <summary>
        /// Loads the settings.
        /// </summary>
        public static void LoadSettings()
        {
            
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