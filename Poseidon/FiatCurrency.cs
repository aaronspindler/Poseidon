using System;
using System.IO;
using System.Net;

namespace Poseidon
{
    public class FiatCurrency
    {
        public FiatCurrency()
        {
            
        }

        public void GetData()
        {
            WebClient client = new WebClient();
            try
            {
                Stream data = client.OpenRead("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
                StreamReader reader = new StreamReader(data);

                Program.WriteToFile(reader.ReadToEnd());
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}
