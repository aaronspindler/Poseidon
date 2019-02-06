﻿// --- Settings.cs ---
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
using System.Reflection;

namespace Poseidon
{
    public static class Settings
    {
        private static string _version;
        private static string _currency;
        private static string _DB_host;
        private static string _DB_port;
        private static string _DB_name;
        private static string _DB_username;
        private static string _DB_password;
        private static string _KRAKEN_KEY;
        private static string _KRAKEN_SIGNATURE;

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
        ///     Gets the Database host.
        /// </summary>
        /// <returns>The Database host.</returns>
        public static string GetDB_Host()
        {
            return _DB_host;
        }

        /// <summary>
        ///     Gets the Database port.
        /// </summary>
        /// <returns>The Database port.</returns>
        public static string GetDB_Port()
        {
            return _DB_port;
        }

        /// <summary>
        ///     Gets the name of the Database.
        /// </summary>
        /// <returns>The Database name.</returns>
        public static string GetDB_Name()
        {
            return _DB_name;
        }

        /// <summary>
        ///     Gets the Database username.
        /// </summary>
        /// <returns>The Database username.</returns>
        public static string GetDB_Username()
        {
            return _DB_username;
        }

        /// <summary>
        ///     Gets the Database password.
        /// </summary>
        /// <returns>The Database password.</returns>
        public static string GetDB_Password()
        {
            return _DB_password;
        }

        /// <summary>
        /// Gets Kraken Key
        /// </summary>
        /// <returns>Kraken Key</returns>
        public static string GetKraken_Key()
        {
            return _KRAKEN_KEY;
        }

        /// <summary>
        /// Gets Kraken Signature
        /// </summary>
        /// <returns>Kraken Signature</returns>
        public static string GetKraken_Signature()
        {
            return _KRAKEN_SIGNATURE;
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
            _version = line.Split('=')[1];

            line = reader.ReadLine();
            _currency = line.Split('=')[1];

            line = reader.ReadLine();
            _DB_host = line.Split('=')[1];

            line = reader.ReadLine();
            _DB_port = line.Split('=')[1];

            line = reader.ReadLine();
            _DB_name = line.Split('=')[1];

            line = reader.ReadLine();
            _DB_username = line.Split('=')[1];

            line = reader.ReadLine();
            _DB_password = line.Split('=')[1];

            line = reader.ReadLine();
            if (line != null && line.Substring(0, 11) == "KRAKEN_KEY=")
                _KRAKEN_KEY = line.Substring(11);

            line = reader.ReadLine();
            if (line != null && line.Substring(0, 17) == "KRAKEN_SIGNATURE=")
                _KRAKEN_SIGNATURE = line.Substring(17);
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
                    sw.WriteLine("DB_HOST=localhost");
                    sw.WriteLine("DB_PORT=3306");
                    sw.WriteLine("DB_NAME=poseidon");
                    sw.WriteLine("DB_USERNAME=xnovax");
                    sw.WriteLine("DB_PASSWORD=temp");
                    sw.WriteLine("KRAKEN_KEY=default");
                    sw.WriteLine("KRAKEN_SIGNATURE=default");

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