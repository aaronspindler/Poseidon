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
            _signature = Settings.GetKraken_Private_Key();
            _url = "https://api.kraken.com";
            _version = 0;
        }

        public void GetServerTime()
        {

        }

        public void GetAssetInfo()
        {

        }

        public void GetTradableAssetPairs()
        {

        }

        public void GetTickerInfo()
        {

        }

        public void GetOHLC()
        {

        }

        public void GetOrderBook()
        {

        }

        public void GetRecentTrades()
        {

        }

        public void GetRecentSpread()
        {

        }

        public void GetAccountBalance()
        {

        }

        public void GetTradeBalance()
        {

        }

        public void GetOpenOrders()
        {

        }

        public void GetClosedOrders()
        {

        }

        public void QueryOrdersInfo()
        {

        }

        public void GetTradesHistory()
        {

        }

        public void QueryTradesInfo()
        {

        }

        public void GetOpenPositions()
        {

        }

        public void GetLedgersInfo()
        {
            
        }

        public void QueryLedgers()
        {
            
        }

        public void GetTradeVolume()
        {
            
        }

        public void AddStandardOrder()
        {
            
        }

        public void CancelOpenOrder()
        {
            
        }
}
}