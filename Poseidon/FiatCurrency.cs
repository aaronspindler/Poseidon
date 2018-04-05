using System;
using System.IO;
using System.Net;
using System.Xml;
using Poseidon.Models.FiatCurrency;

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
                string xmlText = reader.ReadToEnd();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlText);


                Program.WriteToFile(xmlText);
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}
