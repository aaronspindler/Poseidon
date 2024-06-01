using System;
using System.Collections.Generic;
using Poseidon.Models.Kraken;

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

        public List<string> GetTradableAssetPairs()
        {
            var pairsRaw = _kraken.GetAssetPairs().Result;

            var pairs = new List<string>();
            foreach (var pair in pairsRaw)
            {
                pairs.Add(pair.Key);
                Console.WriteLine(pair.Key);
            }

            return pairs;
        }

        public Ticker GetTickerInfo(string pair)
        {
            var tickerInfo = _kraken.GetTicker(pair);
            return tickerInfo;
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