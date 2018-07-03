// --- Utilities.cs ---
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
using System.Net;

namespace Poseidon
{
	/// <summary>
    /// Utilities.
    /// </summary>
    public static class Utilities
	{
		/// <summary>
        /// Exits the program.
        /// </summary>
        public static void ExitProgram()
        {
            Logger.WriteLine("Press enter to close application");
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
            Logger.WriteLine("File written");
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
            Logger.WriteLine("File written");
        }

        /// <summary>
        /// Gets the Unix time and puts it to a string.
        /// </summary>
        /// <returns>The time to string.</returns>
        /// <param name="unix">Unix.</param>
        public static string UnixTimeToString(decimal unix)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds((double)unix).ToLocalTime();
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
                using (Stream stream = client.OpenRead("http://www.google.com"))
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
