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
				XmlDocument xml = new XmlDocument();
				xml.Load("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");

				var nsmgr = new XmlNamespaceManager(xml.NameTable);
				nsmgr.AddNamespace("gesmes", "http://www.gesmes.org/xml/2002-08-01");

				XmlNodeList nodeList = xml.SelectNodes("/gesmes:Envelope/Cube/Cube", nsmgr);

				Console.WriteLine(nodeList.Count);

				foreach(XmlNode node in nodeList){
					Console.WriteLine(node.Attributes.GetNamedItem("currency").Value);
				}
               
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}
