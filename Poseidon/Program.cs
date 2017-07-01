using System;
using System.IO;

namespace Poseidon
{
    class Program
    {
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
