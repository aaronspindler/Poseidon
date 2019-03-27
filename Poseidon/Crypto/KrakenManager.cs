using System;
using System.Collections.Generic;

namespace Poseidon.Crypto
{
    public class KrakenManager
    {
        private readonly KrakenAPI _kraken;
        private readonly int _rateLimitMilliseconds;

        public KrakenManager()
        {
            _kraken = new KrakenAPI();
        }

        public string GetServerTimeFormatted()
        {
            var time = _kraken.GetServerTime().result.rfc1123;
            return time;
        }

        public int GetServerTimeUNIX()
        {
            var time = _kraken.GetServerTime().result.unixtime;
            return time;
        }

        public void GetAssetInfo()
        {
        }

        //TODO: Implement asset pair parsing for new json format
        public void GetTradableAssetPairs()
        {
            var pairs = _kraken.GetAssetPairs().Result;

            foreach (var pair in pairs)
            {
                Console.WriteLine(pair.Key);
                Console.WriteLine(pair.Value.Altname);
            }

            //Console.WriteLine(pairs.Values);
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

        public Dictionary<string, decimal> GetAccountBalance()
        {
            var balances = _kraken.GetAccountBalance().balances;
            return balances;
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