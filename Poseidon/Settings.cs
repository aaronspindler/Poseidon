// --- Settings.cs ---
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

namespace Poseidon
{
    public static class Settings
    {
        static string version;
        static string currency;
        static string DB_host;
        static string DB_port;
        static string DB_name;
        static string DB_username;
        static string DB_password;

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <returns>The version.</returns>
		public static string GetVersion()
        {
            return version;
        }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        /// <returns>The currency.</returns>
		public static string GetCurrency()
        {
            return currency;
        }

        /// <summary>
        /// Gets the Database host.
        /// </summary>
        /// <returns>The Database host.</returns>
		public static string GetDB_Host()
        {
            return DB_host;
        }

        /// <summary>
        /// Gets the Database port.
        /// </summary>
        /// <returns>The Database port.</returns>
		public static string GetDB_Port()
        {
            return DB_port;
        }

        /// <summary>
        /// Gets the name of the Database.
        /// </summary>
        /// <returns>The Database name.</returns>
		public static string GetDB_Name()
        {
            return DB_name;
        }

        /// <summary>
        /// Gets the Database username.
        /// </summary>
        /// <returns>The Database username.</returns>
		public static string GetDB_Username()
        {
            return DB_username;
        }

        /// <summary>
        /// Gets the Database password.
        /// </summary>
        /// <returns>The Database password.</returns>
		public static string GetDB_Password()
        {
            return DB_password;
        }


        /// <summary>
        /// Checks the settings file.
        /// </summary>
        public static void CheckSettingsFile()
        {
            if (!File.Exists("settings.txt"))
            {
                Settings.CreateDefaultSettingsFile();
                Logger.WriteLine("Created default settings file");
            }
        }


        /// <summary>
        /// Loads the settings.
        /// </summary>
        public static void LoadSettings()
        {
            StreamReader reader = new StreamReader("settings.txt");

            string line = reader.ReadLine();
            version = line.Split('=')[1];

            line = reader.ReadLine();
            currency = line.Split('=')[1];

            line = reader.ReadLine();
            DB_host = line.Split('=')[1];

            line = reader.ReadLine();
            DB_port = line.Split('=')[1];

            line = reader.ReadLine();
            DB_name = line.Split('=')[1];

            line = reader.ReadLine();
            DB_username = line.Split('=')[1];

            line = reader.ReadLine();
            DB_password = line.Split('=')[1];
        }


        /// <summary>
        /// Creates a default settings file.
        /// </summary>
        private static void CreateDefaultSettingsFile()
        {
            try
            {
                using (var sw = File.CreateText("settings.txt"))
                {
                    sw.WriteLine("VERSION=" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
                    sw.WriteLine("CURRENCY=CAD");
                    sw.WriteLine("DB_HOST=localhost");
                    sw.WriteLine("DB_PORT=3306");
                    sw.WriteLine("DB_NAME=poseidon");
                    sw.WriteLine("DB_USERNAME=xnovax");
                    sw.WriteLine("DB_PASSWORD=temp");

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
