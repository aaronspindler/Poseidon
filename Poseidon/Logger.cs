#region

using System;
using System.IO;

#endregion

namespace Poseidon
{
    public static class Logger
    {
        private static StreamWriter writer;
        private static string fileName;

        /// <summary>
        ///     Initialize this instance.
        /// </summary>
        public static void Initialize()
        {
            Directory.CreateDirectory("Logs");
            var now = DateTime.Now.Ticks;
            fileName = string.Format(@"Logs/{0}.txt", now);
        }

        /// <summary>
        ///     Writes the input to the console and to a file
        /// </summary>
        /// <param name="input">Input.</param>
        public static void WriteLine(string input)
        {
            var messageText = DateTime.Now + " : " + input;
            WriteLineNoDate(messageText);
        }

        public static void WriteLineNoDate(string input)
        {
            Console.WriteLine(input);

            try
            {
                writer = new StreamWriter(fileName, true);
                writer.WriteLine(input);
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Logger Error: " + e.Message);
            }
        }
    }
}