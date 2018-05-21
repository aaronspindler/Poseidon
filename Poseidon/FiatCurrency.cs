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
		EuropeanCentralBankResponse ecbData;

		/// <summary>
        /// Initializes a new instance of the <see cref="T:Poseidon.FiatCurrency"/> class.
        /// </summary>
        public FiatCurrency()
        {
			ecbData = new EuropeanCentralBankResponse();
        }

        /// <summary>
		/// Gets current data containing exchange rates for fiat currencies from the ECB (European Central Bank)
        /// </summary>
        public void GetEcbData()
        {
			string xmlDataFileName = "test.txt";
            try
			{
				WebClient client = new WebClient();
				Stream data = client.OpenRead("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
				StreamReader reader = new StreamReader(data);
				StreamWriter writer = new StreamWriter(xmlDataFileName);
                //Read the header and ignore it
				for (int i = 0; i < 7; i++){
					reader.ReadLine();
				}

                ///Write the date
				writer.WriteLine(reader.ReadLine().Trim());

                ///Read and write the actual currency data
				for (int i = 0; i < 32; i++){
					writer.WriteLine(reader.ReadLine().Trim());
				}

				///Close reader and writers
				writer.Close();
				reader.Close();

				reader = new StreamReader(xmlDataFileName);

                ///Skip over the date line
				reader.ReadLine();
                
                //Start the loop
				string txt = reader.ReadLine();
				while(txt != null){
					string[] split = txt.Split('\'');
					string name = split[1];
					double rate = Convert.ToDouble(split[3]);

					ecbData.currencies.Add(new Currency(name, rate));
					txt = reader.ReadLine();
				}
				reader.Close();


            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}
