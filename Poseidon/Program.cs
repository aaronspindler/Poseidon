using System;
using System.IO;
using System.Reflection;

namespace Poseidon
{
    internal class Program
    {
        private static string KEY;
        private static string SIGNATURE;

        private static void Main(string[] args)
        {
            Console.WriteLine("Poseidon: " + Assembly.GetEntryAssembly().GetName().Version);

            CheckKeyFile();
            LoadKeys();

            Kraken kraken = new Kraken(KEY, SIGNATURE);
            Console.WriteLine(kraken.GetServerTime().UnixTime);

            Console.ReadLine();
        }

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

        public static void LoadKeys()
        {
            using (var sr = new StreamReader("API.txt"))
            {
                var line = sr.ReadLine();
                if (line.Substring(0, 4) == "KEY=")
                {
                    KEY = line.Substring(4);
                    Console.WriteLine("Key = " + KEY);
                }
                line = sr.ReadLine();
                if (line.Substring(0, 10) == "SIGNATURE=")
                {
                    SIGNATURE = line.Substring(10);
                    Console.WriteLine("Signature = " + SIGNATURE);
                }
            }
        }

        public static void ExitProgram()
        {
            Console.WriteLine("Press enter to close application");
            Console.ReadLine();
            Environment.Exit(-1);
        }
    }
}