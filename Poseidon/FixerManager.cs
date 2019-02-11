using System;
using System.IO;
using System.Net;

namespace Poseidon
{
    public class FixerManager
    {
        public FixerManager()
        {
            
        }

        public void GetFiatRates()
        {
            var client = new WebClient();
            try
            {
                //Rates are all in base EURO for the free api
                string address = "http://data.fixer.io/api/latest?access_key=" + Settings.GetFixer_Key();
                var data = client.OpenRead(address);
                var reader = new StreamReader(data);
                Console.WriteLine(reader.ReadToEnd());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}