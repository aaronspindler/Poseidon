using System;
using System.IO;

namespace Poseidon
{
    class Program
    {
        private static String KEY;
        private static String SIGNATURE;

        static void Main(string[] args)
        {
            Console.WriteLine("Poseidon: " + System.Reflection.Assembly.GetEntryAssembly().GetName().Version);

            if (!File.Exists("API.txt"))
            {
                try
                {
                    using (StreamWriter sw = File.CreateText("API.txt"))
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

            using (StreamReader sr = new StreamReader("API.txt"))
            {
                String line = sr.ReadLine();
                if (line.Substring(0, 4) == "KEY=")
                {
                    KEY = line.Substring(4);
                    Console.WriteLine("Key = " +  KEY);
                }
                line = sr.ReadLine();
                if (line.Substring(0, 10) == "SIGNATURE=")
                {
                    SIGNATURE = line.Substring(10);
                    Console.WriteLine("Signature = " + SIGNATURE);
                }
            }

            Console.ReadLine();
        }

        public static void ExitProgram()
        {
            Console.WriteLine("Press enter to close application");
            Console.ReadLine();
            Environment.Exit(-1);
        }
    }
}
