#region

using System;
using System.Data.SqlTypes;
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
        private static string _version;
        private static string _currency;
        private static string _KRAKEN_KEY;
        private static string _KRAKEN_SIGNATURE;
        private static string _FIXER_KEY;
        private static string _AWS_ACCESS_KEY;
        private static string _AWS_SECRET_KEY;

        /// <summary>
        ///     Gets the version.
        /// </summary>
        /// <returns>The version.</returns>
        public static string GetVersion()
        {
            return _version;
        }

        /// <summary>
        ///     Gets the currency.
        /// </summary>
        /// <returns>The currency.</returns>
        public static string GetCurrency()
        {
            return _currency;
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
        public static string GetKraken_Signature()
        {
            return _KRAKEN_SIGNATURE;
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
        /// Gets the AWS Access Key
        /// </summary>
        /// <returns>AWS Access Key</returns>
        public static string GetAWS_Access_Key()
        {
            return _AWS_ACCESS_KEY;
        }

        /// <summary>
        /// Gets the AWS Secret Key
        /// </summary>
        /// <returns>AWS Secret Key</returns>
        public static string GetAWS_Secret_Key()
        {
            return _AWS_SECRET_KEY;
        }


        /// <summary>
        ///     Checks the settings file.
        /// </summary>
        public static void CheckSettingsFile()
        {
            if (File.Exists("settings.txt")) return;
            CreateDefaultSettingsFile();
            Logger.WriteLine("Created default settings file");
        }


        /// <summary>
        ///     Loads the settings.
        /// </summary>
        public static void LoadSettings()
        {
            var reader = new StreamReader("settings.txt");

            var line = reader.ReadLine();
            if (line != null && line.Substring(0, 8) == "VERSION=")
                _version = line.Substring(8);

            line = reader.ReadLine();
            if (line != null && line.Substring(0, 9) == "CURRENCY=")
                _currency = line.Substring(9);

            line = reader.ReadLine();
            if (line != null && line.Substring(0, 11) == "KRAKEN_KEY=")
                _KRAKEN_KEY = line.Substring(11);

            line = reader.ReadLine();
            if (line != null && line.Substring(0, 17) == "KRAKEN_SIGNATURE=")
                _KRAKEN_SIGNATURE = line.Substring(17);

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


        /// <summary>
        ///     Creates a default settings file.
        /// </summary>
        private static void CreateDefaultSettingsFile()
        {
            try
            {
                using (var sw = File.CreateText("settings.txt"))
                {
                    sw.WriteLine("VERSION=" + Assembly.GetExecutingAssembly().GetName().Version);
                    sw.WriteLine("CURRENCY=CAD");
                    sw.WriteLine("KRAKEN_KEY=default");
                    sw.WriteLine("KRAKEN_SIGNATURE=default");
                    sw.WriteLine("FIXER_KEY=default");
                    sw.WriteLine("AWS_ACCESS_KEY=default");
                    sw.WriteLine("AWS_SECRET_KEY=default");

                    sw.Close();
                }
            }
            catch (Exception exception)
            {
                Logger.WriteLine(exception.Message);

                Utilities.ExitProgram();
            }
        }
    }
}