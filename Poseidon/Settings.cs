using System;
using System.IO;

namespace Poseidon
{
    public class Settings
    {
        public Settings()
        {
			
        }

		/// <summary>
        /// Checks the settings file.
        /// </summary>
        public void CheckSettingsFile()
        {
            if (!File.Exists("settings.txt"))
            {
                CreateDefaultSettingsFile();
            }
            else
            {
                using (var sr = new StreamReader("settings.txt"))
                {
                    var line = sr.ReadLine();
                    if (line.Substring(line.LastIndexOf('='), (line.Length - line.LastIndexOf('='))) !=
                        System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
                    {
                        Console.WriteLine("Looks like you were using a previous version of the settings file.");
                        CreateDefaultSettingsFile();
                        Console.WriteLine("Your settings file has been remade");
                    }
                }
            }
        }

        
        /// <summary>
        /// Loads the settings.
        /// </summary>
        public void LoadSettings()
        {

        }


		/// <summary>
        /// Creates a default settings file.
        /// </summary>
        private void CreateDefaultSettingsFile()
        {
            try
            {
                using (var sw = File.CreateText("settings.txt"))
                {
                    sw.WriteLine("VERSION=" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
                    sw.WriteLine("CURRENCY=CAD");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

                Program.ExitProgram();
            }
        }
    }
}
