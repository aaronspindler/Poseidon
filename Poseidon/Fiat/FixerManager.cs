#region

using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Poseidon.Misc;
using Poseidon.Models.FiatCurrency.Fixer;

#endregion

namespace Poseidon.Fiat
{
    public class FixerManager
    {
        private FixerResponse _response;
        private bool fail;
        
        public void GetFiatRates()
        {
            GetData();
            
            if (fail) return;
            
            Rebase();
            AddToDatabase();
        }

        //TODO: Implement data collection from fixer
        private void GetData()
        {
            fail = false;
            var client = new WebClient();
            try
            {
                //Rates are all in base EURO for the free api
                var address = "http://data.fixer.io/api/latest?access_key=" + Settings.GetFixer_Key();
                var data = client.OpenRead(address);
                var reader = new StreamReader(data);
                var jsonText = reader.ReadToEnd();

                _response = JsonConvert.DeserializeObject<FixerResponse>(jsonText);

                if (_response.success == false)
                {
                    Logger.WriteLine("Fixer Error " + _response.error.code + " : " + _response.error.info);
                    fail = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void AddToDatabase()
        {
            Database.CreateFixerEntry(_response.entry);
        }


        //TODO: Implement rebasing for fixer data
        private void Rebase()
        {
        }
    }
}