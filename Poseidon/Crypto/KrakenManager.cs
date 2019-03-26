namespace Poseidon.Crypto
{
    public class KrakenManager
    {
        private readonly int _rateLimitMilliseconds;
        private readonly string _key;
        private readonly string _signature;
        private readonly string _url;
        private readonly int _version;

        /// <summary>
        /// Constructor for Kraken
        /// </summary>
        /// <param name="rateLimitMilliseconds">Time to wait for polling data</param>
        public KrakenManager(int rateLimitMilliseconds = 100)
        {
            _key = Settings.GetKraken_Key();
            _signature = Settings.GetKraken_Signature();
            _url = "https://api.kraken.com";
            _version = 0;
            
        }
    }
}