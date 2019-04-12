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
        /// Returns a DateTime provided a unixtime
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(double unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long) (unixTime * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
        }
        
        /// <summary>
        /// Returns the current UnixTime from a provided DateTime
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (dateTime.ToUniversalTime() - unixStart).Ticks;
            return (double) unixTimeStampInTicks / TimeSpan.TicksPerSecond;
        }

        /// <summary>
        /// Returns a accurate string representation of a UNIX time
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public static string UnixTimestampToString(double unixTime)
        {
            return UnixTimestampToDateTime(unixTime).ToString("MM/dd/yyyy HH:mm:ss");
        }
        
        /// <summary>
        /// Returns current UnixTime
        /// </summary>
        /// <returns></returns>
        public static long GetUNIXTime()
        {
            var dt = DateTime.Now; 
            var unixTime = ((DateTimeOffset)dt).ToUnixTimeSeconds();
            return unixTime;
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

        /// <summary>
        /// Generates a unique identifier
        /// </summary>
        /// <returns>Unique ID</returns>
        public static string GenerateGUID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}