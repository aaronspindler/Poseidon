#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Poseidon.Misc;
using Poseidon.Models.Kraken;
using Poseidon.Models.Old;

#endregion

namespace Poseidon.Crypto
{
    /// <summary>
    ///     Main class for interacting with Kraken.com
    /// </summary>
    public class Kraken
    {
        private readonly string _key;
        private readonly int _rateLimitMilliseconds;
        private readonly string _secret;
        private readonly string _url;
        private readonly int _version;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Kraken" /> class.
        /// </summary>
        /// <param name="rateLimitMilliseconds">The rate limit in milliseconds.</param>
        public Kraken(int rateLimitMilliseconds = 100)
        {
            _url = "https://api.kraken.com";
            _version = 0;
            _rateLimitMilliseconds = rateLimitMilliseconds;
            //TODO: Check if credentials are valid
            _key = Settings.GetKraken_Key();
            _secret = Settings.GetKraken_Signature();
        }


        private string BuildPostData(Dictionary<string, string> param)
        {
            if (param == null)
                return "";

            var b = new StringBuilder();
            foreach (var item in param)
                b.Append(string.Format("&{0}={1}", item.Key, item.Value));

            try
            {
                return b.ToString().Substring(1);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string QueryPublic(string method, Dictionary<string, string> param = null)
        {
            RateLimit();

            var address = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/public/{2}", _url, _version, method);
            var webRequest = (HttpWebRequest) WebRequest.Create(new Uri(address));
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";

            var postData = BuildPostData(param);

            if (!string.IsNullOrEmpty(postData))
                using (var writer = new StreamWriter(webRequest.GetRequestStream()))
                {
                    writer.Write(postData);
                }

            try
            {
                using (var webResponse = webRequest.GetResponse())
                {
                    var str = webResponse.GetResponseStream();
                    using (var sr = new StreamReader(str))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch (WebException wex)
            {
                using (var response = (HttpWebResponse) wex.Response)
                {
                    if (response == null)
                        throw;

                    var str = response.GetResponseStream();
                    using (var sr = new StreamReader(str))
                    {
                        if (response.StatusCode != HttpStatusCode.InternalServerError)
                            throw;
                        return sr.ReadToEnd();
                    }
                }
            }
        }

        private string QueryPrivate(string method, Dictionary<string, string> param = null)
        {
            RateLimit();

            // generate a 64 bit nonce using a timestamp at tick resolution
            var nonce = DateTime.UtcNow.Ticks;

            var postData = BuildPostData(param);
            if (!string.IsNullOrEmpty(postData))
                postData = "&" + postData;
            postData = "nonce=" + nonce + postData;

            var path = string.Format(CultureInfo.InvariantCulture, "/{0}/private/{1}", _version, method);
            var address = _url + path;
            var webRequest = (HttpWebRequest) WebRequest.Create(address);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";

            AddHeaders(webRequest, nonce, postData, path);

            if (postData != null)
                using (var writer = new StreamWriter(webRequest.GetRequestStream()))
                {
                    writer.Write(postData);
                }

            //Make the request
            try
            {
                using (var webResponse = webRequest.GetResponse())
                {
                    var str = webResponse.GetResponseStream();
                    using (var sr = new StreamReader(str))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch (WebException wex)
            {
                using (var response = (HttpWebResponse) wex.Response)
                {
                    var str = response.GetResponseStream();
                    if (str == null)
                        throw;

                    using (var sr = new StreamReader(str))
                    {
                        if (response.StatusCode != HttpStatusCode.InternalServerError)
                            throw;
                        return sr.ReadToEnd();
                    }
                }
            }
        }


        private void AddHeaders(HttpWebRequest webRequest, long nonce, string postData, string path)
        {
            webRequest.Headers.Add("API-Key", _key);

            var base64DecodedSecred = Convert.FromBase64String(_secret);

            var np = nonce + Convert.ToChar(0) + postData;

            var pathBytes = Encoding.UTF8.GetBytes(path);
            var hash256Bytes = sha256_hash(np);
            var z = new byte[pathBytes.Count() + hash256Bytes.Count()];
            pathBytes.CopyTo(z, 0);
            hash256Bytes.CopyTo(z, pathBytes.Count());

            var signature = getHash(base64DecodedSecred, z);

            webRequest.Headers.Add("API-Sign", Convert.ToBase64String(signature));
        }

        #region Public Market Data

        /// <summary>
        ///     Gets the server time.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="KrakenException"></exception>
        public ServerTime GetServerTime()
        {
            var res = QueryPublic("Time");
            try
            {
                var ret = JsonConvert.DeserializeObject<ServerTime>(res);
                return ret;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        ///     Gets the asset information.
        /// </summary>
        /// <returns></returns>
        public AssetInfo GetAssetInfo()
        {
            var res = QueryPublic("Assets");
            try
            {
                var ret = JsonConvert.DeserializeObject<AssetInfo>(res);
                return ret;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        ///     Gets the asset pairs.
        ///     Note: If an asset pair is on a maker/taker fee schedule, the taker side is given in
        ///     "fees" and maker side in "fees_maker". For pairs not on maker/taker, they will only be given in "fees".
        /// </summary>
        /// <returns></returns>
        /// <exception cref="KrakenException"></exception>
        public AssetPairs GetAssetPairs()
        {
            var res = QueryPublic("AssetPairs");
            try
            {
                var ret = JsonConvert.DeserializeObject<AssetPairs>(res);
                return ret;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        ///     Gets the ticker info.
        /// </summary>
        /// <param name="pair">Comma delimited list of asset pairs to get info on</param>
        public Ticker GetTicker(string pair)
        {
            var param = new Dictionary<string, string>();
            param.Add("pair", pair);
            var res = QueryPublic("Ticker", param);
            Utilities.WriteToFile(res);
            var ret = new Ticker();
            try
            {
                var obj = (JObject) JsonConvert.DeserializeObject(res);
                var result = obj["result"].Value<JObject>();
                var pairLevel = result[pair].Value<JObject>();

                var ask = pairLevel["a"].Value<JArray>().ToArray();
                ret.ask.price = (decimal) ask[0];
                ret.ask.wholeVolume = (decimal) ask[1];
                ret.ask.volume = (decimal) ask[2];

                var bid = pairLevel["b"].Value<JArray>();
                ret.bid.price = (decimal) bid[0];
                ret.bid.wholeVolume = (decimal) bid[1];
                ret.bid.volume = (decimal) bid[2];

                var lastClosed = pairLevel["c"].Value<JArray>();
                ret.lastClosed.price = (decimal) lastClosed[0];
                ret.lastClosed.volume = (decimal) lastClosed[1];

                var volume = pairLevel["v"].Value<JArray>();
                ret.volume.today = (decimal) volume[0];
                ret.volume.last24hours = (decimal) volume[1];

                var volumeWeightedByPrice = pairLevel["p"].Value<JArray>();
                ret.volumeWeightedByPrice.today = (decimal) volumeWeightedByPrice[0];
                ret.volumeWeightedByPrice.last24hours = (decimal) volumeWeightedByPrice[1];

                var numTrades = pairLevel["t"].Value<JArray>();
                ret.numTrades.today = (decimal) numTrades[0];
                ret.numTrades.last24hours = (decimal) numTrades[1];

                var low = pairLevel["l"].Value<JArray>();
                ret.low.today = (decimal) low[0];
                ret.low.last24hours = (decimal) low[1];

                var high = pairLevel["h"].Value<JArray>();
                ret.high.today = (decimal) high[0];
                ret.high.last24hours = (decimal) high[1];

                var opening = pairLevel["o"].Value<string>();
                ret.opening = Convert.ToDecimal(opening);

                return ret;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        ///     Gets the ohlc.
        /// </summary>
        /// <param name="pair">The pair.</param>
        public OHLCSet GetOHLC(string pair)
        {
            var param = new Dictionary<string, string>();
            param.Add("pair", pair);
            var res = QueryPublic("OHLC", param);

            var obj = (JObject) JsonConvert.DeserializeObject(res);
            var result = obj["result"].Value<JObject>();

            var ret = new OHLCSet();
            try
            {
                ret.Pairs = new List<OHLC>();

                foreach (var o in result)
                    if (o.Key != "last")
                        foreach (var v in o.Value.ToObject<decimal[][]>())
                            ret.Pairs.Add(new OHLC
                            {
                                Time = (int) v[0],
                                Open = v[1],
                                High = v[2],
                                Low = v[3],
                                Close = v[4],
                                Vwap = v[5],
                                Volume = v[6],
                                Count = (int) v[7]
                            });
            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex.Message);
                throw;
            }

            return ret;
        }

        /// <summary>
        ///     Gets the order book.
        /// </summary>
        /// <param name="pair">The pair.</param>
        public OrderBook GetOrderBook(string pair)
        {
            var param = new Dictionary<string, string>();
            param.Add("pair", pair);

            var res = QueryPublic("Depth", param);
            var obj = (JObject) JsonConvert.DeserializeObject(res);

            var ret = new OrderBook();
            try
            {
                ret.asks = new List<OrderBookOrder>();
                ret.bids = new List<OrderBookOrder>();

                var result = obj["result"].Value<JObject>();
                var pairLevel = result[pair].Value<JObject>();

                var asks = pairLevel["asks"].Value<JArray>();

                foreach (var o in asks)
                {
                    var j = 0;
                    var order = new OrderBookOrder();
                    foreach (var i in o)
                    {
                        if (j == 0)
                            order.price = (decimal) i;
                        else if (j == 1)
                            order.volume = (decimal) i;
                        else if (j == 2)
                            order.timestamp = (decimal) i;
                        j++;
                    }

                    ret.asks.Add(order);
                }

                var bids = pairLevel["bids"].Value<JArray>();

                foreach (var o in bids)
                {
                    var j = 0;
                    var order = new OrderBookOrder();
                    foreach (var i in o)
                    {
                        if (j == 0)
                            order.price = (decimal) i;
                        else if (j == 1)
                            order.volume = (decimal) i;
                        else if (j == 2)
                            order.timestamp = (decimal) i;
                        j++;
                    }

                    ret.bids.Add(order);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex.Message);
                throw;
            }

            return ret;
        }

        /// <summary>
        ///     Gets the recent trades.
        /// </summary>
        /// <param name="pair">The pair.</param>
        public RecentTrades GetRecentTrades(string pair)
        {
            var param = new Dictionary<string, string>();
            param.Add("pair", pair);

            var res = QueryPublic("Trades", param);

            var obj = (JObject) JsonConvert.DeserializeObject(res);

            var result = obj["result"].Value<JObject>();
            var trades = result[pair].Value<JArray>();

            var ret = new RecentTrades();
            ret.trades = new List<Trade>();
            foreach (var t in trades)
                ret.trades.Add(new Trade
                    {
                        price = t[0].Value<decimal>(),
                        volume = t[1].Value<decimal>(),
                        time = t[2].Value<decimal>(),
                        buysell = t[3].Value<char>(),
                        marketlimit = t[4].Value<char>(),
                        misc = t[5].Value<string>()
                    }
                );

            return ret;
        }

        /// <summary>
        ///     Gets the recent spread.
        ///     Note: "since" is inclusive so any returned data with the same time as the
        ///     previous set should overwrite all of the previous set's entries at that time.
        /// </summary>
        /// <param name="pair">The pair.</param>
        public RecentSpread GetRecentSpread(string pair)
        {
            var param = new Dictionary<string, string>();
            param.Add("pair", pair);

            var res = QueryPublic("Spread", param);

            var obj = (JObject) JsonConvert.DeserializeObject(res);

            var result = obj["result"].Value<JObject>();
            var spread = result[pair].Value<JArray>();

            var ret = new RecentSpread();
            ret.spread = new List<SpreadItem>();

            foreach (var s in spread)
                ret.spread.Add(new SpreadItem
                {
                    time = s[0].Value<decimal>(),
                    ask = s[1].Value<decimal>(),
                    bid = s[2].Value<decimal>()
                });

            return ret;
        }

        #endregion Public Market Data

        #region Private User Data

        /// <summary>
        ///     Gets the account balance.
        /// </summary>
        /// <returns></returns>
        public AccountBalance GetAccountBalance()
        {
            var res = QueryPrivate("Balance");
            var obj = (JObject) JsonConvert.DeserializeObject(res);
            var ret = new AccountBalance();
            ret.balances = new Dictionary<string, decimal>();

            var temp = obj["result"].Value<JObject>();
            ret.balances = temp.ToObject<Dictionary<string, decimal>>();
            return ret;
        }

        /// <summary>
        ///     Gets the trade balance.
        /// </summary>
        /// <param name="aclass">The asset class (optional) currency (default)</param>
        /// <param name="asset">Base asset used to determine balance (default = ZUSD).</param>
        /// <returns></returns>
        /// <exception cref="KrakenException"></exception>
        public TradeBalanceInfo GetTradeBalance(string aclass = null, string asset = null)
        {
            var param = new Dictionary<string, string>();
            if (aclass != null)
                param.Add("aclass", aclass);
            if (asset != null)
                param.Add("asset", asset);

            var res = QueryPrivate("TradeBalance");
            var ret = JsonConvert.DeserializeObject<GetTradeBalanceResponse>(res);
            if (ret.Error.Count != 0)
                throw new KrakenException(ret.Error[0], ret);
            return ret.Result;
        }

        /// <summary>
        ///     Gets the open orders.
        /// </summary>
        /// <param name="trades">Whether or not to include trades in output (optional.  default = false).</param>
        /// <param name="userref">Restrict results to given user reference id (optional).</param>
        /// <exception cref="KrakenException"></exception>
        public Dictionary<string, OrderInfo> GetOpenOrders(bool? trades = null, string userref = null)
        {
            var param = new Dictionary<string, string>();
            if (trades != null)
                param.Add("trades", trades.ToString().ToLower());
            if (userref != null)
                param.Add("userref", userref);

            var res = QueryPrivate("OpenOrders");

            var obj = (JObject) JsonConvert.DeserializeObject(res);
            var err = (JArray) obj["error"];
            if (err.Count != 0)
                throw new KrakenException(err[0].ToString(), JsonConvert.DeserializeObject<ResponseBase>(res));

            var open = obj["result"]["open"].Value<JObject>();

            var ret = new Dictionary<string, OrderInfo>();
            foreach (var o in open)
                ret.Add(o.Key, o.Value.ToObject<OrderInfo>());

            return ret;
        }

        /// <summary>
        ///     Gets the closed orders.
        /// </summary>
        /// <param name="trades">Whether or not to include trades in output (optional.  default = false)</param>
        /// <param name="userref">Restrict results to given user reference id (optional).</param>
        /// <param name="start">Starting unix timestamp or order tx id of results (optional.  exclusive).</param>
        /// <param name="end">Ending unix timestamp or order tx id of results (optional.  inclusive).</param>
        /// <param name="ofs">Result offset.</param>
        /// <param name="closetime">
        ///     Which time to use (optional)
        ///     open
        ///     close
        ///     both(default).
        /// </param>
        /// <returns></returns>
        /// <exception cref="KrakenException"></exception>
        public Dictionary<string, OrderInfo> GetClosedOrders(
            bool? trades = null,
            string userref = null,
            int? start = null,
            int? end = null,
            int? ofs = null,
            string closetime = null)
        {
            var param = new Dictionary<string, string>();
            if (trades != null)
                param.Add("trades", trades.ToString().ToLower());
            if (userref != null)
                param.Add("userref", userref);

            var res = QueryPrivate("ClosedOrders");

            var obj = (JObject) JsonConvert.DeserializeObject(res);
            var err = (JArray) obj["error"];
            if (err.Count != 0)
                throw new KrakenException(err[0].ToString(), JsonConvert.DeserializeObject<ResponseBase>(res));

            var open = obj["result"]["closed"].Value<JObject>();

            var ret = new Dictionary<string, OrderInfo>();
            foreach (var o in open)
                ret.Add(o.Key, o.Value.ToObject<OrderInfo>());

            return ret;
        }

        /// <summary>
        ///     Queries the orders.
        /// </summary>
        /// <param name="txid">Transaction ids to query info about (20 maximum).</param>
        /// <param name="trades">Whether or not to include trades in output (optional.  default = false).</param>
        /// <param name="userref">Restrict results to given user reference id (optional).</param>
        public Dictionary<string, OrderInfo> QueryOrder(string txid, bool? trades = null, string userref = null)
        {
            return QueryOrders(new[] {txid}, trades, userref);
        }

        /// <summary>
        ///     Queries the orders.
        /// </summary>
        /// <param name="txid">Transaction ids to query info about (20 maximum).</param>
        /// <param name="trades">Whether or not to include trades in output (optional.  default = false).</param>
        /// <param name="userref">Restrict results to given user reference id (optional).</param>
        public Dictionary<string, OrderInfo> QueryOrders(IEnumerable<string> txid, bool? trades = null,
            string userref = null)
        {
            var param = new Dictionary<string, string>();
            if (trades != null)
                param.Add("trades", trades.ToString().ToLower());
            if (userref != null)
                param.Add("userref", userref);
            param.Add("txid", string.Join(",", txid));

            var res = QueryPrivate("QueryOrders", param);
            var ret = JsonConvert.DeserializeObject<QueryOrdersResponse>(res);
            if (ret.Error.Count != 0)
                throw new KrakenException(ret.Error[0], ret);
            return ret.Result;
        }

        /// <summary>
        ///     Gets the trades history.
        /// </summary>
        /// <param name="type">
        ///     Type of trade (optional)
        ///     all = all types(default)
        ///     any position = any position(open or closed)
        ///     closed position = positions that have been closed
        ///     closing position = any trade closing all or part of a position
        ///     no position = non - positional trades
        /// </param>
        /// <param name="trades">Whether or not to include trades related to position in output (optional.  default = false).</param>
        /// <param name="start">Starting unix timestamp or trade tx id of results (optional.  exclusive).</param>
        /// <param name="end">Ending unix timestamp or trade tx id of results (optional.  inclusive).</param>
        /// <param name="ofs">Result offset.</param>
        /// <returns></returns>
        public GetTradesHistoryResult GetTradesHistory(string type = null, bool? trades = null, int? start = null,
            int? end = null, int? ofs = null)
        {
            var param = new Dictionary<string, string>();
            if (type != null)
                param.Add("type", type);
            if (trades != null)
                param.Add("trades", trades.ToString().ToLower());
            if (start != null)
                param.Add("start", start.ToString());
            if (end != null)
                param.Add("end", end.ToString());
            if (ofs != null)
                param.Add("ofs", ofs.ToString());

            var res = QueryPrivate("TradesHistory", param);
            var ret = JsonConvert.DeserializeObject<GetTradesHistoryResponse>(res);
            if (ret.Error.Count != 0)
                throw new KrakenException(ret.Error[0], ret);
            return ret.Result;
        }

        /// <summary>
        ///     Queries the trades.
        /// </summary>
        /// <param name="txid">Transaction id to query info about.</param>
        /// <param name="trades">Whether or not to include trades related to position in output (optional.  default = false).</param>
        public Dictionary<string, TradeInfo> QueryTrades(string txid, bool? trades = null)
        {
            return QueryTrades(new[] {txid}, trades);
        }

        /// <summary>
        ///     Queries the trades.
        /// </summary>
        /// <param name="txid">Transaction ids to query info about (20 maximum).</param>
        /// <param name="trades">Whether or not to include trades related to position in output (optional.  default = false).</param>
        public Dictionary<string, TradeInfo> QueryTrades(IEnumerable<string> txid, bool? trades = null)
        {
            var param = new Dictionary<string, string>();
            if (trades != null)
                param.Add("trades", trades.ToString().ToLower());
            param.Add("txid", string.Join(",", txid));

            var res = QueryPrivate("QueryTrades", param);
            var ret = JsonConvert.DeserializeObject<QueryTradesResponse>(res);
            if (ret.Error.Count != 0)
                throw new KrakenException(ret.Error[0], ret);
            return ret.Result;
        }

        /// <summary>
        ///     Gets the open positions.
        /// </summary>
        /// <param name="txid">Transaction ids to restrict output to.</param>
        /// <param name="docalcs">Whether or not to include profit/loss calculations (optional.  default = false).</param>
        public Dictionary<string, PositionInfo> GetOpenPositions(IEnumerable<string> txid, bool? docalcs = null)
        {
            var param = new Dictionary<string, string>();
            if (docalcs != null)
                param.Add("docalcs", docalcs.ToString().ToLower());
            param.Add("txid", string.Join(",", txid));

            var res = QueryPrivate("OpenPositions", param);
            var ret = JsonConvert.DeserializeObject<GetOpenPositionsResponse>(res);
            if (ret.Error.Count != 0)
                throw new KrakenException(ret.Error[0], ret);
            return ret.Result;
        }

        /// <summary>
        ///     Gets the ledgers.
        /// </summary>
        /// <param name="aclass">
        ///     asset class (optional):
        ///     currency(default).
        /// </param>
        /// <param name="asset">List of assets to restrict output to (optional.  default = all).</param>
        /// <param name="type">
        ///     type of ledger to retrieve (optional):
        ///     all(default)
        ///     deposit
        ///     withdrawal
        ///     trade
        ///     margin
        /// </param>
        /// <param name="start">Starting unix timestamp or ledger id of results (optional.  exclusive).</param>
        /// <param name="end">Ending unix timestamp or ledger id of results (optional.  inclusive).</param>
        /// <param name="ofs">Result offset.</param>
        public GetLedgerResult GetLedgers(
            string aclass = null,
            IEnumerable<string> asset = null,
            string type = null,
            int? start = null,
            int? end = null,
            int? ofs = null)
        {
            var param = new Dictionary<string, string>();
            if (aclass != null)
                param.Add("aclass", aclass);
            if (asset != null)
                param.Add("asset", string.Join(",", asset));
            if (type != null)
                param.Add("type", type);
            if (start != null)
                param.Add("start", start.ToString());
            if (end != null)
                param.Add("end", end.ToString());
            if (ofs != null)
                param.Add("ofs", ofs.ToString());

            var res = QueryPrivate("Ledgers", param);
            var ret = JsonConvert.DeserializeObject<GetLedgerResponse>(res);
            if (ret.Error.Count != 0)
                throw new KrakenException(ret.Error[0], ret);
            return ret.Result;
        }

        /// <summary>
        ///     Queries the ledgers.
        /// </summary>
        /// <param name="id">List of ledger ids to query info about (20 maximum).</param>
        public Dictionary<string, LedgerInfo> QueryLedgers(IEnumerable<string> id)
        {
            var param = new Dictionary<string, string>();
            param.Add("id", string.Join(",", id));

            var res = QueryPrivate("QueryLedgers", param);
            var ret = JsonConvert.DeserializeObject<QueryLedgersResponse>(res);
            if (ret.Error.Count != 0)
                throw new KrakenException(ret.Error[0], ret);
            return ret.Result;
        }

        /// <summary>
        ///     Gets the trade volume.
        /// </summary>
        /// <param name="pair">List of asset pairs to get fee info on (optional).</param>
        /// <param name="feeInfo">Whether or not to include fee info in results (optional).</param>
        public GetTradeVolumeResult GetTradeVolume(IEnumerable<string> pair = null, bool? feeInfo = null)
        {
            var param = new Dictionary<string, string>();
            if (pair != null)
                param.Add("pair", string.Join(",", pair));
            if (feeInfo != null)
                param.Add("fee-info", feeInfo.ToString().ToLower());

            var res = QueryPrivate("TradeVolume", param);
            var ret = JsonConvert.DeserializeObject<GetTradeVolumeResponse>(res);
            if (ret.Error.Count != 0)
                throw new KrakenException(ret.Error[0], ret);
            return ret.Result;
        }

        #endregion Private User Data

//
//        #region Private User Trading
//
//        public AddOrderResult AddOrder(KrakenOrder order)
//        {
//            var param = new Dictionary<string, string>();
//            param.Add("pair", order.Pair);
//            param.Add("type", order.Type);
//            param.Add("ordertype", order.OrderType);
//            if (order.Price != null)
//                param.Add("price", order.Price.Value.ToString(CultureInfo.InvariantCulture));
//            if (order.Price2 != null)
//                param.Add("price2", order.Price2.Value.ToString(CultureInfo.InvariantCulture));
//            param.Add("volume", order.Volume.ToString(CultureInfo.InvariantCulture));
//            if (order.Leverage != null)
//                param.Add("leverage", order.Leverage.Value.ToString(CultureInfo.InvariantCulture));
//            if (order.OFlags != null)
//                param.Add("oflags", order.OFlags);
//            if (order.StartTm != null)
//                param.Add("starttm", order.StartTm.ToString());
//            if (order.ExpireTm != null)
//                param.Add("expiretm", order.ExpireTm.ToString());
//            if (order.UserRef != null)
//                param.Add("userref", order.UserRef.ToString());
//            if (order.Validate != null)
//                param.Add("validate", order.Validate.ToString().ToLower());
//
//            if (order.Close != null)
//            {
//                param.Add("close[ordertype]", order.Close["ordertype"]);
//                param.Add("close[price]", order.Close["price"]);
//                param.Add("close[price2]", order.Close["price2"]);
//            }
//
//            var res = QueryPrivate("AddOrder", param);
//            var ret = JsonConvert.DeserializeObject<AddOrderResponse>(res);
//            if (ret.Error.Count != 0)
//                throw new KrakenException(ret.Error[0], ret);
//
//            order.Txid = ret.Result.Txid.Select(x => x).ToArray();
//            order.Descr = new AddOrderDescr {Order = ret.Result.Descr.Order, Close = ret.Result.Descr.Close};
//
//            return ret.Result;
//        }
//
//        /// <summary>
//        ///     Cancels the order.
//        /// </summary>
//        /// <param name="txid">
//        ///     Transaction id.
//        ///     Note: txid may be a user reference id.
//        /// </param>
//        public CancelOrderResult CancelOrder(string txid)
//        {
//            var param = new Dictionary<string, string>();
//            param.Add("txid", txid);
//
//            var res = QueryPrivate("CancelOrder", param);
//            var ret = JsonConvert.DeserializeObject<CancelOrderResponse>(res);
//            if (ret.Error.Count != 0)
//                throw new KrakenException(ret.Error[0], ret);
//            return ret.Result;
//        }
//
//        #endregion Private User Trading
//
//        #region Private User Funding
//
//        /// <summary>
//        ///     Gets the deposit methods.
//        /// </summary>
//        /// <param name="aclass">
//        ///     Asset class (optional):
//        ///     currency(default).
//        /// </param>
//        /// <param name="asset">Asset being deposited.</param>
//        public GetDepositMethodsResult[] GetDepositMethods(string aclass = null, string asset = null)
//        {
//            var param = new Dictionary<string, string>();
//            if (aclass != null)
//                param.Add("aclass", aclass);
//            if (asset != null)
//                param.Add("asset", asset);
//
//            var res = QueryPrivate("DepositMethods", param);
//            var ret = JsonConvert.DeserializeObject<GetDepositMethodsResponse>(res);
//            if (ret.Error.Count != 0)
//                throw new KrakenException(ret.Error[0], ret);
//            return ret.Result;
//        }
//
//        /// <summary>
//        ///     Gets the deposit addresses.
//        /// </summary>
//        /// <param name="asset">Asset being deposited.</param>
//        /// <param name="method">Name of the deposit method.</param>
//        /// <param name="aclass">
//        ///     Asset class (optional):
//        ///     currency(default).
//        /// </param>
//        /// <param name="new">Whether or not to generate a new address (optional.  default = false).</param>
//        public GetDepositAddressesResult GetDepositAddresses(string asset, string method, string aclass = null, bool? @new = null)
//        {
//            var param = new Dictionary<string, string>();
//            param.Add("asset", asset);
//            param.Add("method", method);
//            if (aclass != null)
//                param.Add("aclass", aclass);
//            if (@new != null)
//                param.Add("new", @new.ToString().ToLower());
//
//            var res = QueryPrivate("DepositAddresses", param);
//            var ret = JsonConvert.DeserializeObject<GetDepositAddressesResponse>(res);
//            if (ret.Error.Count != 0)
//                throw new KrakenException(ret.Error[0], ret);
//            return ret.Result;
//        }
//
//        /// <summary>
//        ///     Gets the deposit status.
//        /// </summary>
//        /// <param name="asset">Asset being deposited.</param>
//        /// <param name="method">Name of the deposit method.</param>
//        /// <param name="aclass">
//        ///     Asset class (optional):
//        ///     currency(default).
//        /// </param>
//        /// <returns></returns>
//        public GetDepositStatusResult[] GetDepositStatus(string asset, string method, string aclass = null)
//        {
//            var param = new Dictionary<string, string>();
//            param.Add("asset", asset);
//            param.Add("method", method);
//            if (aclass != null)
//                param.Add("aclass", aclass);
//
//            var res = QueryPrivate("DepositStatus", param);
//            var ret = JsonConvert.DeserializeObject<GetDepositStatusResponse>(res);
//            if (ret.Error.Count != 0)
//                throw new KrakenException(ret.Error[0], ret);
//            return ret.Result;
//        }
//
//        /// <summary>
//        ///     Gets the withdraw information.
//        /// </summary>
//        /// <param name="asset">Asset being withdrawn.</param>
//        /// <param name="key">Withdrawal key name, as set up on your account.</param>
//        /// <param name="amount">Amount to withdraw.</param>
//        /// <param name="aclass">
//        ///     Asset class (optional):
//        ///     currency(default).
//        /// </param>
//        /// <returns></returns>
//        public GetWithdrawInfoResult GetWithdrawInfo(string asset, string key, decimal amount, string aclass = null)
//        {
//            var param = new Dictionary<string, string>();
//            param.Add("asset", asset);
//            param.Add("key", key);
//            param.Add("amount", amount.ToString(CultureInfo.InvariantCulture));
//            if (aclass != null)
//                param.Add("aclass", aclass);
//
//            var res = QueryPrivate("WithdrawInfo", param);
//            var ret = JsonConvert.DeserializeObject<GetWithdrawInfoResponse>(res);
//            if (ret.Error.Count != 0)
//                throw new KrakenException(ret.Error[0], ret);
//            return ret.Result;
//        }
//
//        /// <summary>
//        ///     Withdraws the specified asset.
//        /// </summary>
//        /// <param name="asset">Asset being withdrawn.</param>
//        /// <param name="key">Withdrawal key name, as set up on your account.</param>
//        /// <param name="amount">Amount to withdraw.</param>
//        /// <param name="aclass">
//        ///     Asset class (optional):
//        ///     currency(default).
//        /// </param>
//        /// <returns>The reference id.</returns>
//        public string Withdraw(string asset, string key, decimal amount, string aclass = null)
//        {
//            var param = new Dictionary<string, string>();
//            param.Add("asset", asset);
//            param.Add("key", key);
//            param.Add("amount", amount.ToString(CultureInfo.InvariantCulture));
//            if (aclass != null)
//                param.Add("aclass", aclass);
//
//            var res = QueryPrivate("Withdraw", param);
//            var ret = JsonConvert.DeserializeObject<WithdrawResponse>(res);
//            if (ret.Error.Count != 0)
//                throw new KrakenException(ret.Error[0], ret);
//            return ret.Result.RefId;
//        }
//
//        /// <summary>
//        ///     Gets the withdraw status.
//        /// </summary>
//        /// <param name="asset">Asset being withdrawn.</param>
//        /// <param name="method">Withdrawal method name (optional).</param>
//        /// <param name="aclass">
//        ///     Asset class (optional):
//        ///     currency(default).
//        /// </param>
//        /// <returns></returns>
//        public GetWithdrawStatusResult GetWithdrawStatus(string asset, string method, string aclass = null)
//        {
//            var param = new Dictionary<string, string>();
//            param.Add("asset", asset);
//            param.Add("method", method);
//            if (aclass != null)
//                param.Add("aclass", aclass);
//
//            var res = QueryPrivate("WithdrawStatus", param);
//            var ret = JsonConvert.DeserializeObject<GetWithdrawStatusResponse>(res);
//            if (ret.Error.Count != 0)
//                throw new KrakenException(ret.Error[0], ret);
//            return ret.Result;
//        }
//
//        /// <summary>
//        ///     Cancel the withdrawal.
//        ///     Note: Cancelation cannot be guaranteed. This will put in a cancelation request.
//        ///     Depending upon how far along the withdrawal process is, it may not be possible to cancel the withdrawal.
//        /// </summary>
//        /// <param name="asset">Asset being withdrawn.</param>
//        /// <param name="refid">Withdrawal reference id.</param>
//        /// <param name="aclass">
//        ///     Asset class (optional):
//        ///     currency(default).
//        /// </param>
//        public bool WithdrawCancel(string asset, string refid, string aclass = null)
//        {
//            var param = new Dictionary<string, string>();
//            param.Add("asset", asset);
//            param.Add("refid", refid);
//            if (aclass != null)
//                param.Add("aclass", aclass);
//
//            var res = QueryPrivate("WithdrawCancel", param);
//            var ret = JsonConvert.DeserializeObject<WithdrawCancelResponse>(res);
//            if (ret.Error.Count != 0)
//                throw new KrakenException(ret.Error[0], ret);
//            return ret.Result;
//        }
//
//        #endregion Private User Funding

        #region Helper methods

        private byte[] sha256_hash(string value)
        {
            using (var hash = SHA256.Create())
            {
                return hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
        }

        private byte[] getHash(byte[] keyByte, byte[] messageBytes)
        {
            using (var hmacsha512 = new HMACSHA512(keyByte))
            {
                return hmacsha512.ComputeHash(messageBytes);
            }
        }

        #endregion Helper methods

        #region Rate limiter

        private long lastTicks;
        private readonly object thisLock = new object();

        private void RateLimit()
        {
            lock (thisLock)
            {
                var elapsedTicks = DateTime.Now.Ticks - lastTicks;
                var elapsedSpan = new TimeSpan(elapsedTicks);
                if (elapsedSpan.TotalMilliseconds < _rateLimitMilliseconds)
                    Thread.Sleep(_rateLimitMilliseconds - (int) elapsedSpan.TotalMilliseconds);
                lastTicks = DateTime.Now.Ticks;
            }
        }

        #endregion Rate limiter
    }
}