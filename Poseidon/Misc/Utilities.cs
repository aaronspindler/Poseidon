#region

using System;
using System.IO;
using System.Net;

#endregion

namespace Poseidon.Misc
{
    /// <summary>
    ///     Utilities.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        ///     Exits the program.
        /// </summary>
        public static void ExitProgram(bool notify)
        {
            if (notify)
            {
                Logger.WriteLine("Press enter to close application");
                Console.ReadLine();
            }
            Environment.Exit(-1);
        }
        

        /// <summary>
        ///     Writes to a file.
        /// </summary>
        /// <param name="text">The text thats written to the file</param>
        public static void WriteToFile(string text)
        {
            using (var sw = File.CreateText("temp.txt"))
            {
                sw.Write(text);
            }

            Logger.WriteLine("File written");
        }

        /// <summary>
        ///     Writes to a file.
        /// </summary>
        /// <param name="fileName">File name of where to write the text</param>
        /// <param name="text">The text thats written toa  file</param>
        public static void WriteToFile(string fileName, string text)
        {
            using (var sw = File.CreateText(fileName + "_" + DateTime.Now.ToFileTime() + ".txt"))
            {
                sw.Write(text);
            }

            Logger.WriteLine("File written");
        }

        /// <summary>
        ///     Gets the Unix time and puts it to a string.
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
        public static bool CheckNetworkConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}