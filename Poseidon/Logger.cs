#region

using System;
using System.IO;

#endregion

namespace Poseidon
{
    /// <summary>
    ///     The class used to log all input and output to the console
    /// </summary>
    public static class Logger
    {
        private static StreamWriter _writer;
        private static string _fileName;

        /// <summary>
        ///     Initialize this instance.
        /// </summary>
        public static void Initialize()
        {
            Directory.CreateDirectory("Logs");
            var now = DateTime.Now.Ticks;
            _fileName = string.Format(@"Logs/{0}.txt", now);
        }

        /// <summary>
        ///     Writes the date + input to the console and to a file
        /// </summary>
        /// <param name="input">Input.</param>
        public static void WriteLine(string input)
        {
            var messageText = DateTime.Now + " : " + input;
            WriteLineNoDate(messageText);
        }

        /// <summary>
        ///     Writes the input to the console and to a file
        /// </summary>
        /// <param name="input"></param>
        public static void WriteLineNoDate(string input)
        {
            Console.WriteLine(input);

            try
            {
                _writer = new StreamWriter(_fileName, true);
                _writer.WriteLine(input);
                _writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Logger Error: " + e.Message);
            }
        }
    }
}