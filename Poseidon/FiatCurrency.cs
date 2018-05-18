using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Poseidon.Models.FiatCurrency;

namespace Poseidon
{
    public class FiatCurrency
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="T:Poseidon.FiatCurrency"/> class.
        /// </summary>
        public FiatCurrency()
        {
            
        }

        /// <summary>
        /// Gets current data containing exchange rates for fiat currencies
        /// </summary>
        public void GetData()
        {
            try
			{
				WebClient client = new WebClient();
				Stream data = client.OpenRead("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
				StreamReader reader = new StreamReader(data);
				StreamWriter writer = new StreamWriter("xmltext.txt");
                //Read the header and ignore it
				for (int i = 0; i < 7; i++){
					reader.ReadLine();
				}

                //Write the date
				writer.WriteLine(reader.ReadLine());

                //Read the actual currency data
				for (int i = 0; i < 32; i++){
					writer.WriteLine(reader.ReadLine());
				}

				writer.Close();
				reader.Close();

               
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}
