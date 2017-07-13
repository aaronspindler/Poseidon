using System;
using System.Net;
using Newtonsoft.Json;
using Poseidon.Models.Public;

namespace Poseidon
{
    internal class KrakenPublic
    {
        public static int GetServerTime()
        {
            var wc = new WebClient();
            //wc.Headers.Add("X-TBA-App-Id", Settings.Default.Header_Address + Assembly.GetExecutingAssembly().GetName().Version);
            try
            {
                var url = "https://api.kraken.com/0/public/Time";
                var downloadedData = wc.DownloadString(url);

                var times = JsonConvert.DeserializeObject<ServerTime>(downloadedData);

                return times.result.unixtime;
            }
            catch (Exception webError)
            {
                Console.WriteLine(webError.Message);
                Program.ExitProgram();
            }

            return -1;
        }
    }
}