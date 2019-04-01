#region

using System;
using System.IO;
using System.Reflection;
using Poseidon.Misc;

#endregion

namespace Poseidon
{
    /// <summary>
    ///     Stores, Parses, and Utilizes settings.txt
    /// </summary>
    public static class Settings
    {
        private static string _VERSION;
        private static string _CURRENCY;
        private static string _KRAKEN_KEY;
        private static string _KRAKEN_PRIVATE_KEY;
        private static string _FIXER_KEY;
        private static string _AWS_ACCESS_KEY;
        private static string _AWS_SECRET_KEY;

        /// <summary>
        ///     Initialize the settings
        /// </summary>
        public static void Startup(string[] args)
        {
            if (args.Length > 0)
            {
                if (args.Length != 6)
                {
                    Logger.WriteLine("Please double check your command line arguments");
                    Logger.WriteLine("Example: ./Poseidon.dll {base_currency} {kraken_key} {kraken_private_key} {fixer_key} {aws_key} {aws_secret_key}");
                    Utilities.ExitProgram(false);
                }
                ParseFromArgs(args);
            }
            else
            {
                CheckSettingsFile("settings.txt");
                ParseFromFile("settings.txt");
            }
        }

        /// <summary>
        ///     Gets the version.
        /// </summary>
        /// <returns>The version.</returns>
        public static string GetVersion()
        {
            return _VERSION;
        }

        /// <summary>
        ///     Gets the currency.
        /// </summary>
        /// <returns>The currency.</returns>
        public static string GetCurrency()
        {
            return _CURRENCY;
        }

        /// <summary>
        ///     Gets Kraken Key
        /// </summary>
        /// <returns>Kraken Key</returns>
        public static string GetKraken_Key()
        {
            return _KRAKEN_KEY;
        }

        /// <summary>
        ///     Gets Kraken Signature
        /// </summary>
        /// <returns>Kraken Signature</returns>
        public static string GetKraken_Private_Key()
        {
            return _KRAKEN_PRIVATE_KEY;
        }

        /// <summary>
        ///     Gets the fixer api key
        /// </summary>
        /// <returns>Fixer API Key</returns>
        public static string GetFixer_Key()
        {
            return _FIXER_KEY;
        }

        /// <summary>
        ///     Gets the AWS Access Key
        /// </summary>
        /// <returns>AWS Access Key</returns>
        public static string GetAWS_Access_Key()
        {
            return _AWS_ACCESS_KEY;
        }

        /// <summary>
        ///     Gets the AWS Secret Key
        /// </summary>
        /// <returns>AWS Secret Key</returns>
        public static string GetAWS_Secret_Key()
        {
            return _AWS_SECRET_KEY;
        }


        /// <summary>
        ///     Checks the settings file.
        /// </summary>
        private static void CheckSettingsFile(string fileName)
        {
            if (File.Exists(fileName)) return;
            CreateDefaultSettingsFile(fileName);
            Logger.WriteLineNoDate("Created default settings file -> settings.txt");
            Logger.WriteLineNoDate("Please fill out the settings and restart the program");
            Utilities.ExitProgram(false);
        }

        /// <summary>
        ///     Deletes a settings file
        ///     Used for testing
        /// </summary>
        /// <param name="fileName">The location of the settings file you want to delete</param>
        private static void DeleteSettingsFile(string fileName)
        {
            File.Delete(fileName);
        }

        /// <summary>
        ///     Loads settings from settings file
        /// </summary>
        /// <param name="fileName">Filename for settings file</param>
        private static void ParseFromFile(string fileName)
        {
            var reader = new StreamReader(fileName);

            var line = reader.ReadLine();
            if (line != null && line.Substring(0, 8) == "VERSION=")
                _VERSION = line.Substring(8);

            line = reader.ReadLine();
            if (line != null && line.Substring(0, 9) == "CURRENCY=")
                _CURRENCY = line.Substring(9);

            line = reader.ReadLine();
            if (line != null && line.Substring(0, 11) == "KRAKEN_KEY=")
                _KRAKEN_KEY = line.Substring(11);

            line = reader.ReadLine();
            if (line != null && line.Substring(0, 19) == "KRAKEN_PRIVATE_KEY=")
                _KRAKEN_PRIVATE_KEY = line.Substring(19);

            line = reader.ReadLine();
            if (line != null && line.Substring(0, 10) == "FIXER_KEY=")
                _FIXER_KEY = line.Substring(10);

            line = reader.ReadLine();
            if (line != null && line.Substring(0, 15) == "AWS_ACCESS_KEY=")
                _AWS_ACCESS_KEY = line.Substring(15);

            line = reader.ReadLine();
            if (line != null && line.Substring(0, 15) == "AWS_SECRET_KEY=")
                _AWS_SECRET_KEY = line.Substring(15);
        }


        //TODO: Make this safe
        /// <summary>
        ///     Parses command line arguments to settings
        /// </summary>
        /// <param name="args">Command line arguments</param>
        private static void ParseFromArgs(string[] args)
        {
            _CURRENCY = args[0];
            _KRAKEN_KEY = args[1];
            _KRAKEN_PRIVATE_KEY = args[2];
            _FIXER_KEY = args[3];
            _AWS_ACCESS_KEY = args[4];
            _AWS_SECRET_KEY = args[5];
        }

        /// <summary>
        ///     Creates a default settings file
        /// </summary>
        /// <param name="fileName">filename for settings file</param>
        private static void CreateDefaultSettingsFile(string fileName)
        {
            CreateSettingsFile(fileName, "CAD", "default", "default", "default", "default", "default");
        }

        /// <summary>
        ///     Creates a settings file from variables
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="krakenKey"></param>
        /// <param name="krakenPrivateKey"></param>
        /// <param name="fixerKey"></param>
        /// <param name="awsKey"></param>
        /// <param name="awsSecretKey"></param>
        private static void CreateSettingsFile(string fileName, string currency, string krakenKey,
            string krakenPrivateKey, string fixerKey, string awsKey, string awsSecretKey)
        {
            try
            {
                using (var sw = File.CreateText(fileName))
                {
                    sw.WriteLine("VERSION=" + Assembly.GetExecutingAssembly().GetName().Version);
                    sw.WriteLine("CURRENCY=" + currency);
                    sw.WriteLine("KRAKEN_KEY=" + krakenKey);
                    sw.WriteLine("KRAKEN_PRIVATE_KEY=" + krakenPrivateKey);
                    sw.WriteLine("FIXER_KEY=" + fixerKey);
                    sw.WriteLine("AWS_ACCESS_KEY=" + awsKey);
                    sw.WriteLine("AWS_SECRET_KEY=" + awsSecretKey);
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine(e.Message);
                Utilities.ExitProgram(false);
            }
        }
    }
}