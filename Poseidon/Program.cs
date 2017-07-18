using System;
using System.IO;
using System.Reflection;
using Console = Colorful.Console;

namespace Poseidon
{
    public class Program
    {
        private static string KEY;
        private static string SIGNATURE;

        private static void Main(string[] args)
        {
            Console.WriteLine("Poseidon: " + Assembly.GetEntryAssembly().GetName().Version);

            CheckKeyFile();
            LoadKeys();

            var kraken = new Kraken(KEY, SIGNATURE);
            Console.WriteLine("Login Successful");
            Console.WriteLine(kraken.GetServerTime().result.rfc1123);

            //var recent = kraken.GetRecentTrades(kraken.GetAssetPairs().result.GNOUSD.altname);
            kraken.GetTicker(kraken.GetAssetPairs().result.GNOUSD.altname);

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
                    KEY = line.Substring(4);
                line = sr.ReadLine();
                if (line.Substring(0, 10) == "SIGNATURE=")
                    SIGNATURE = line.Substring(10);
            }
        }

        public static void ExitProgram()
        {
            Console.WriteLine("Press enter to close application");
            Console.ReadLine();
            Environment.Exit(-1);
        }

        public static void WriteToFile(string text)
        {
            using (var sw = File.CreateText("temp.txt"))
            {
                sw.Write(text);
            }
            Console.WriteLine("File written");
        }
    }
}