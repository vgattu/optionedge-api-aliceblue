///

// Development Test Class
// This is used to test the specific features as they are implemented

// If you are lookng for api samples, refer to FeaturesDemo.cs class

///
using Newtonsoft.Json;
using OptionEdge.API.AliceBlue.Records;
using System.Collections.Concurrent;
using System.Text;

namespace OptionEdge.API.AliceBlue.Samples
{
    public class DevTest
    {
        public static int niftyBankInstrumentToken = 26009;
        public static int indiaVixToken = 26017;
        public static int indiaVix = 0;
        public static int indiaVixThreshold = 3000;
        public static int putInstrumentToken;
        public static int callInstrumentToken;
        public static TimeSpan MarketStartTime = new(9, 12, 0);
        public static TimeSpan OrderPlacementTime = new(9, 29, 59);
        public static TimeSpan MarketCloseTime = new(14, 59, 59);
        public static BankNiftyStrikePrice currentStrikeData = new();
        public static SubscriptionInstruments currentSubscriptions = new();
        public static Dictionary<decimal, Tuple<int, int>> strikeInstrumentTokenDict = new(); //Call & Put
        public static bool isNiftyBankTickerClosed;
        public static string putSellOrderOpenId;
        public static string callSellOrderOpenId;
        public static string putBuyOrderOpenId;
        public static string callBuyOrderOpenId;
        public static string putBuyOrderCloseId;
        public static string callBuyOrderCloseId;
        private static string orderTag = "test";
        private static int bankNiftyLotSize = 25;
        private static int aliceBlueNumberOfLots1 = 13;
        public static int ceSellPrice, ceBuyPrice, ceBuyClosePrice, peSellPrice, peBuyPrice, peBuyClosePrice;
        public static bool ceSellTriggered, ceBuyTriggered, peSellTriggered, peBuyTriggered, ceBuyClosed, peBuyClosed;
        public static int cePnL, pePnL, maxPnL;
        public static bool maxPnLCrossedThreshold;
        public static bool allPositionsClosed;
        public static DateTime expiryDate = new DateTime(2022, 12, 22);

        public static bool LowerRisk200Straddle = true;

        public static bool MultiStraddle = false;

        public static bool createOrder = true;
        public static bool createRealOrder = true;
        public static bool startingFreeze = true;
        public static int startingFreezeCounter = 5;
        public static TimeSpan lastUpdated = DateTime.Now.TimeOfDay;
        public static int bankNiftyEntryLtp = 0;
        public static int multiStraddleTrailPnl = 0;
        public static decimal buyLimitPrice = 400.00m;
        public static bool createCeOrder = true;
        public static bool createPeOrder = true;
        public static List<int> ceInstrumentList = new();
        public static List<int> peInstrumentList = new();
        public static TimeSpan removeLots = DateTime.Now.TimeOfDay;
        public static TimeSpan addLots = DateTime.Now.TimeOfDay;
        public static Dictionary<int, string> instrumentDict;
        public static ConcurrentQueue<ConsoleKey> consoleKeyQueue = new();
        public static TimeSpan lastTickerFeed = DateTime.Now.TimeOfDay;

        // apiKey, userId, logging setting
        static Settings _settings = new Settings();


        public static AliceBlue _aliceBlue;
        public static Ticker _ticker;

        static string _cachedTokenFile = $"cached_token{DateTime.Now:yyyyMMMdd}.txt";

        public void Run()
        {
            try
            {
                //strikeInstrumentTokenDict.Add(37500, new Tuple<int, int>(51300, 51301));
                //strikeInstrumentTokenDict.Add(38000, new Tuple<int, int>(51307, 51308));
                //strikeInstrumentTokenDict.Add(38500, new Tuple<int, int>(51309, 51310));
                //strikeInstrumentTokenDict.Add(39000, new Tuple<int, int>(51329, 51330));
                //strikeInstrumentTokenDict.Add(39500, new Tuple<int, int>(51339, 51340));
                //strikeInstrumentTokenDict.Add(40000, new Tuple<int, int>(51349, 51350));
                //strikeInstrumentTokenDict.Add(40300, new Tuple<int, int>(51359, 51361));
                //strikeInstrumentTokenDict.Add(40400, new Tuple<int, int>(51363, 51364));
                //strikeInstrumentTokenDict.Add(40500, new Tuple<int, int>(51365, 51366));
                //strikeInstrumentTokenDict.Add(40600, new Tuple<int, int>(51367, 51368));
                //strikeInstrumentTokenDict.Add(40700, new Tuple<int, int>(51369, 51370));
                //strikeInstrumentTokenDict.Add(40800, new Tuple<int, int>(51371, 51372));
                //strikeInstrumentTokenDict.Add(40900, new Tuple<int, int>(51373, 51376));
                //strikeInstrumentTokenDict.Add(41000, new Tuple<int, int>(51377, 51378));
                //strikeInstrumentTokenDict.Add(41100, new Tuple<int, int>(51379, 51380));
                //strikeInstrumentTokenDict.Add(41200, new Tuple<int, int>(51381, 51384));
                //strikeInstrumentTokenDict.Add(41300, new Tuple<int, int>(51385, 51386));
                //strikeInstrumentTokenDict.Add(41400, new Tuple<int, int>(51387, 51388));
                //strikeInstrumentTokenDict.Add(41500, new Tuple<int, int>(51389, 51391));
                //strikeInstrumentTokenDict.Add(41600, new Tuple<int, int>(51392, 51393));
                //strikeInstrumentTokenDict.Add(41700, new Tuple<int, int>(51394, 51395));
                //strikeInstrumentTokenDict.Add(41800, new Tuple<int, int>(51396, 51397));
                //strikeInstrumentTokenDict.Add(41900, new Tuple<int, int>(51398, 51407));
                strikeInstrumentTokenDict.Add(42000, new Tuple<int, int>(51408, 51409));
                strikeInstrumentTokenDict.Add(42100, new Tuple<int, int>(51410, 51411));
                strikeInstrumentTokenDict.Add(42200, new Tuple<int, int>(51412, 51413));
                strikeInstrumentTokenDict.Add(42300, new Tuple<int, int>(51414, 51415));
                strikeInstrumentTokenDict.Add(42400, new Tuple<int, int>(51416, 51417));
                strikeInstrumentTokenDict.Add(42500, new Tuple<int, int>(51418, 51419));
                strikeInstrumentTokenDict.Add(42600, new Tuple<int, int>(51420, 51422));
                strikeInstrumentTokenDict.Add(42700, new Tuple<int, int>(51423, 51424));
                strikeInstrumentTokenDict.Add(42800, new Tuple<int, int>(51429, 51430));
                strikeInstrumentTokenDict.Add(42900, new Tuple<int, int>(51431, 51432));
                strikeInstrumentTokenDict.Add(43000, new Tuple<int, int>(51433, 51438));
                strikeInstrumentTokenDict.Add(43100, new Tuple<int, int>(51443, 51454));
                strikeInstrumentTokenDict.Add(43200, new Tuple<int, int>(51456, 51457));
                strikeInstrumentTokenDict.Add(43300, new Tuple<int, int>(51458, 51459));
                strikeInstrumentTokenDict.Add(43400, new Tuple<int, int>(51461, 51462));
                strikeInstrumentTokenDict.Add(43500, new Tuple<int, int>(51464, 51465));
                strikeInstrumentTokenDict.Add(43600, new Tuple<int, int>(51466, 51467));
                strikeInstrumentTokenDict.Add(43700, new Tuple<int, int>(51468, 51469));
                strikeInstrumentTokenDict.Add(43800, new Tuple<int, int>(51470, 51471));
                strikeInstrumentTokenDict.Add(43900, new Tuple<int, int>(51472, 51473));
                strikeInstrumentTokenDict.Add(44000, new Tuple<int, int>(51474, 51475));
                strikeInstrumentTokenDict.Add(44100, new Tuple<int, int>(51476, 51477));
                strikeInstrumentTokenDict.Add(44200, new Tuple<int, int>(51478, 51479));
                strikeInstrumentTokenDict.Add(44300, new Tuple<int, int>(51480, 51481));
                strikeInstrumentTokenDict.Add(44400, new Tuple<int, int>(51482, 51483));
                strikeInstrumentTokenDict.Add(44500, new Tuple<int, int>(51484, 51485));
                strikeInstrumentTokenDict.Add(44600, new Tuple<int, int>(51486, 51491));
                strikeInstrumentTokenDict.Add(44700, new Tuple<int, int>(51492, 51493));
                strikeInstrumentTokenDict.Add(44800, new Tuple<int, int>(51494, 51495));
                strikeInstrumentTokenDict.Add(44900, new Tuple<int, int>(51496, 51497));
                strikeInstrumentTokenDict.Add(45000, new Tuple<int, int>(51498, 51499));
                //strikeInstrumentTokenDict.Add(45100, new Tuple<int, int>(51501, 51502));
                //strikeInstrumentTokenDict.Add(45200, new Tuple<int, int>(51506, 51507));
                //strikeInstrumentTokenDict.Add(45300, new Tuple<int, int>(51508, 51509));
                //strikeInstrumentTokenDict.Add(45400, new Tuple<int, int>(51511, 51512));
                //strikeInstrumentTokenDict.Add(45500, new Tuple<int, int>(51515, 51516));
                //strikeInstrumentTokenDict.Add(45600, new Tuple<int, int>(51517, 51518));
                //strikeInstrumentTokenDict.Add(45700, new Tuple<int, int>(51520, 51523));
                //strikeInstrumentTokenDict.Add(45800, new Tuple<int, int>(51524, 51525));
                //strikeInstrumentTokenDict.Add(45900, new Tuple<int, int>(51526, 51527));
                //strikeInstrumentTokenDict.Add(46000, new Tuple<int, int>(51528, 51529));
                //strikeInstrumentTokenDict.Add(46500, new Tuple<int, int>(51542, 51544));
                //strikeInstrumentTokenDict.Add(47000, new Tuple<int, int>(51545, 51547));
                //strikeInstrumentTokenDict.Add(47500, new Tuple<int, int>(51548, 51549));
                //strikeInstrumentTokenDict.Add(48000, new Tuple<int, int>(51552, 51553));
                //strikeInstrumentTokenDict.Add(48500, new Tuple<int, int>(51554, 51555));
                //strikeInstrumentTokenDict.Add(49000, new Tuple<int, int>(51563, 51564));

                foreach (var keyVal in strikeInstrumentTokenDict)
                {
                    ceInstrumentList.Add(keyVal.Value.Item1);
                    peInstrumentList.Add(keyVal.Value.Item2);
                }

                while (DateTime.Now.TimeOfDay < MarketStartTime)
                {
                    WriteMsgToConsole(DateTime.Now.ToString("HH:mm:ss") + " Waiting....");
                    Thread.Sleep(10000);
                }

                // Read ApiKey, userId from Settings 
#pragma warning disable CS8601 // Possible null reference assignment.
                _settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("Settings.json"));
#pragma warning restore CS8601 // Possible null reference assignment.
                //_settings.ApiKey = Environment.GetEnvironmentVariable("ALICE_BLUE_API_KEY");
                //_settings.UserId = Environment.GetEnvironmentVariable("ALICE_BLUE_USER_ID");
                //_settings.EnableLogging = true;

                // ==========================
                // Initialize
                // ==========================

                // Create new instance of AliceBlue client library
                _aliceBlue = new AliceBlue(_settings.UserId, _settings.ApiKey, enableLogging: _settings.EnableLogging,
                    onAccessTokenGenerated: (accessToken) =>
                    {
                        // Store the generated access token to database or file store
                        // Token needs to be generated only once for the day

                        File.WriteAllText(_cachedTokenFile, accessToken);
                    }, cachedAccessTokenProvider: () =>
                    {
                        // Provide the saved token that will be used to making the REST calls.
                        // This method will be used when re-initializing the api client during the the day (eg after app restart etc)

                        // If token is invalid or not available, just return empty or null value
                        return File.Exists(_cachedTokenFile) ? File.ReadAllText(_cachedTokenFile) : null;
                    });

                Console.WriteLine("Retrieving master contract data...");
                instrumentDict = _aliceBlue.GetMasterContracts(Constants.EXCHANGE_NFO).Result.Where(a => a.InstrumentSymbol == "BANKNIFTY")
                    .ToDictionary(a => a.InstrumentToken, b => b.InstrumentName);
                Console.WriteLine("Done...");
                //var bncontracts = contracts.Where(a => a.InstrumentSymbol == "BANKNIFTY" && a.Expiry == new DateTime(2022, 9, 15)).ToList();
                //var atmContractToken = contracts.Where(a => a.InstrumentSymbol == "BANKNIFTY" && a.Expiry == new DateTime(2022, 9, 15) && a.Strike == 40800).FirstOrDefault()?.InstrumentToken;
                // ==========================
                // Live Feeds Data Streaming
                // ==========================

                // Create Ticker instance
                // No need to provide the userId, apiKey, it will be automatically set
                _ticker = _aliceBlue.CreateTicker();

                //// Setup event handlers
                _ticker.OnTick += _ticker_OnTick;
                _ticker.OnConnect += _ticker_OnConnect;
                _ticker.OnClose += _ticker_OnClose;
                _ticker.OnError += _ticker_OnError;
                _ticker.OnNoReconnect += _ticker_OnNoReconnect;
                _ticker.OnReconnect += _ticker_OnReconnect;

                // Connect the ticker to start receiving the live feeds
                // DO NOT FORGOT TO CONNECT else you will not receive any feed

                _ticker.Connect();
                Thread.Sleep(1000);
                _ticker.EnableReconnect(interval: 5, retries: 20);

                //var openInterest = _aliceBlue.GetOpenInterest(Constants.EXCHANGE_NFO, new int[] { 35042, 37342 });

                //if (atmContractToken != null)
                //    _ticker.Subscribe(Constants.EXCHANGE_NFO, Constants.TICK_MODE_FULL, new int[] { atmContractToken.Value });
                _ticker.Subscribe(Constants.EXCHANGE_NSE, Constants.TICK_MODE_FULL, new int[] { niftyBankInstrumentToken, indiaVixToken });
                if (LowerRisk200Straddle || MultiStraddle)
                {
                    foreach (var keyVal in strikeInstrumentTokenDict)
                    {
                        currentSubscriptions.Add(keyVal.Value.Item1, OptionType.Call, keyVal.Key);
                        currentSubscriptions.Add(keyVal.Value.Item2, OptionType.Put, keyVal.Key);
                    }
                }
                Console.WriteLine("Ticker connected and subscribed...");
                while (true)
                {
                    if (!startingFreeze && (DateTime.Now.TimeOfDay - lastTickerFeed).TotalSeconds > 10)
                    {
                        _ticker.Reconnect();
                        startingFreeze = true;
                        startingFreezeCounter = 5;
                    }
                    //if ((DateTime.Now.TimeOfDay - lastUpdated).TotalSeconds > 30)
                    //{
                    //    wsClient.Start().Wait();
                    //    wsClient.Send("{\"a\": \"subscribe\", \"v\": [[1, " + niftyBankInstrumentToken + "]], \"m\": \"" + AliceBlueMode.marketdata.ToString() + "\"}");
                    //    wsClient.Send("{\"a\": \"subscribe\", \"v\": [[1, " + indiaVixToken + "]], \"m\": \"" + AliceBlueMode.marketdata.ToString() + "\"}");
                    //    if (LowerRisk200Straddle || MultiStraddle)
                    //    {
                    //        foreach (var keyVal in strikeInstrumentTokenDict)
                    //        {
                    //            currentSubscriptions.Remove(keyVal.Value.Item1, wsClient);
                    //            currentSubscriptions.Remove(keyVal.Value.Item2, wsClient);
                    //        }
                    //        foreach (var keyVal in strikeInstrumentTokenDict)
                    //        {
                    //            currentSubscriptions.Add(keyVal.Value.Item1, OptionType.Call, keyVal.Key, wsClient);
                    //            currentSubscriptions.Add(keyVal.Value.Item2, OptionType.Put, keyVal.Key, wsClient);
                    //        }
                    //        startingFreeze = true;
                    //        startingFreezeCounter = 5;
                    //    }
                    //}
                    if (!string.IsNullOrEmpty(putSellOrderOpenId) || !string.IsNullOrEmpty(callSellOrderOpenId))
                    {
                        var ceDetails = currentSubscriptions.Subscriptions.Values.Where(a => a.StrikePrice == currentStrikeData.StrikePrice && a.OptionType == OptionType.Call).FirstOrDefault();
                        var cePrice = ceDetails == null ? 0 : ceDetails.SellPrice;
                        var peDetails = currentSubscriptions.Subscriptions.Values.Where(a => a.StrikePrice == currentStrikeData.StrikePrice && a.OptionType == OptionType.Put).FirstOrDefault();
                        var pePrice = peDetails == null ? 0 : peDetails.SellPrice;
                        WriteMsgToConsole($"{DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss")} " +
                            $"PnL: {(multiStraddleTrailPnl + pePnL + cePnL) / 100m,-10} " +
                            $"Net: {(multiStraddleTrailPnl + pePnL + cePnL) * aliceBlueNumberOfLots1 / 100m,-10} " +
                            $"MaxPnL: {maxPnL / 100m,-10} " +
                            $"Straddle: {(cePrice + pePrice) / 100m,-10} " +
                            $"Update: {((DateTime.Now.TimeOfDay - lastTickerFeed).TotalSeconds>2?">>>>":"")}{(DateTime.Now.TimeOfDay - lastTickerFeed).TotalSeconds:F2}");
                    }
                    if (currentStrikeData?.ExchangeTimeStamp != null && string.IsNullOrEmpty(putSellOrderOpenId) && string.IsNullOrEmpty(callSellOrderOpenId))
                    {
                        var ceDetails = currentSubscriptions.Subscriptions.Values.Where(a => a.StrikePrice == currentStrikeData.StrikePrice && a.OptionType == OptionType.Call).FirstOrDefault();
                        var cePrice = ceDetails == null ? 0 : ceDetails.SellPrice;
                        var peDetails = currentSubscriptions.Subscriptions.Values.Where(a => a.StrikePrice == currentStrikeData.StrikePrice && a.OptionType == OptionType.Put).FirstOrDefault();
                        var pePrice = peDetails == null ? 0 : peDetails.SellPrice;
                        var msg = new StringBuilder();
                        msg.Append($"{DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss")} - {currentStrikeData.StrikePrice} - {(cePrice + pePrice) / 100m,-10} - {indiaVix,-6}");
                        if (LowerRisk200Straddle || MultiStraddle)
                        {
                            var callDiff = 10000000;
                            var putDiff = 10000000;
                            var callToken = 0;
                            var putToken = 0;
                            foreach (var optionDataKeyVal in currentSubscriptions.Subscriptions)
                            {
                                if (optionDataKeyVal.Value.OptionType == OptionType.Call)
                                {
                                    if (Math.Abs(20000 - optionDataKeyVal.Value.SellPrice) < callDiff)
                                    {
                                        callDiff = Math.Abs(20000 - optionDataKeyVal.Value.SellPrice);
                                        callToken = optionDataKeyVal.Key;
                                    }
                                }
                                else
                                {
                                    if (Math.Abs(20000 - optionDataKeyVal.Value.SellPrice) < putDiff)
                                    {
                                        putDiff = Math.Abs(20000 - optionDataKeyVal.Value.SellPrice);
                                        putToken = optionDataKeyVal.Key;
                                    }
                                }
                            }

                            msg.Append($"CE: {currentSubscriptions.Subscriptions[callToken].StrikePrice} - {currentSubscriptions.Subscriptions[callToken].SellPrice / 100m,-10}");
                            msg.Append($"PE: {currentSubscriptions.Subscriptions[putToken].StrikePrice} - {currentSubscriptions.Subscriptions[putToken].SellPrice / 100m,-10} ");
                            msg.Append($"Update: {((DateTime.Now.TimeOfDay - lastTickerFeed).TotalSeconds > 2 ? ">>>>" : "")}{(DateTime.Now.TimeOfDay - lastTickerFeed).TotalSeconds:F2}");

                            //if (string.IsNullOrEmpty(putSellOrderOpenId))
                            //{
                            //    putSellOrderOpenId = SellOpenAtTriggerPrice(Client.AliceBlue, putToken, Helper.NormalizeTickPrice(currentSubscriptions.Subscriptions[putToken].SellPrice - 500), bankNiftyLotSize * aliceBlueNumberOfLots1);
                            //    putBuyOrderOpenId = BuyOpenAtTriggerPrice(Client.AliceBlue, putToken, Helper.NormalizeTickPrice((currentSubscriptions.Subscriptions[putToken].SellPrice - 500) * 125 / 100), bankNiftyLotSize * aliceBlueNumberOfLots1 * 2);
                            //    //putSellOrderOpenId = SellOpenAtTriggerPrice(Client.PayTM, putToken, Helper.NormalizeTickPrice(currentSubscriptions.Subscriptions[putToken].SellPrice - 500), bankNiftyLotSize * paytmNumberOfLots);
                            //    //putBuyOrderOpenId = BuyOpenAtTriggerPrice(Client.PayTM, putToken, Helper.NormalizeTickPrice((currentSubscriptions.Subscriptions[putToken].SellPrice - 500) * 125 / 100), bankNiftyLotSize * paytmNumberOfLots * 2);
                            //    //putSellOrderOpenId = "test";
                            //    //putBuyOrderOpenId = "test";
                            //}
                            //if (string.IsNullOrEmpty(callSellOrderOpenId))
                            //{
                            //    callSellOrderOpenId = SellOpenAtTriggerPrice(Client.AliceBlue, callToken, Helper.NormalizeTickPrice(currentSubscriptions.Subscriptions[callToken].SellPrice - 500), bankNiftyLotSize * aliceBlueNumberOfLots1);
                            //    callBuyOrderOpenId = BuyOpenAtTriggerPrice(Client.AliceBlue, callToken, Helper.NormalizeTickPrice((currentSubscriptions.Subscriptions[callToken].SellPrice - 500) * 125 / 100), bankNiftyLotSize * aliceBlueNumberOfLots1 * 2);
                            //    //callSellOrderOpenId = SellOpenAtTriggerPrice(Client.PayTM, callToken, Helper.NormalizeTickPrice(currentSubscriptions.Subscriptions[callToken].SellPrice - 500), bankNiftyLotSize * paytmNumberOfLots);
                            //    //callBuyOrderOpenId = BuyOpenAtTriggerPrice(Client.PayTM, callToken, Helper.NormalizeTickPrice((currentSubscriptions.Subscriptions[callToken].SellPrice - 500) * 125 / 100), bankNiftyLotSize * paytmNumberOfLots * 2);
                            //    //callSellOrderOpenId = "test";
                            //    //callBuyOrderOpenId = "test";
                            //}
                        }
                        WriteMsgToConsole(msg.ToString());
                    }
                    ConsoleKey k = ConsoleKey.F16;
                    for (int cnt = 30; cnt > 0; cnt--)
                    {
                        if (Console.KeyAvailable)
                        {
                            k = Console.ReadKey().Key;
                            //WriteMsgToConsole((int)k.Key);
                            break;
                        }
                        else if (!consoleKeyQueue.IsEmpty)
                        {
                            consoleKeyQueue.TryDequeue(out k);
                            WriteMsgToConsole("Key received: " + k.ToString());
                        }
                        else
                        {
                            Thread.Sleep(100);
                        }
                    }
                    if (k == ConsoleKey.Z && !allPositionsClosed && createOrder)
                    {
                        CloseAllOpenPositions(Client.AliceBlue);
                        allPositionsClosed = false;
                        WriteMsgToConsole("All open positions closed!!");
                        createOrder = false;
                    }
                    if (k == ConsoleKey.A && !allPositionsClosed && !createOrder)
                    {
                        createOrder = true;
                        ReopenClosePositions(Client.AliceBlue);
                        WriteMsgToConsole("Reopened all closed positions");
                    }
                    if (k == ConsoleKey.R)
                    {
                        if (createRealOrder)
                        {
                            createRealOrder = false;
                            WriteMsgToConsole("Stopped creating real orders");
                        }
                        else
                        {
                            createRealOrder = true;
                            WriteMsgToConsole("Resumed creating real orders");
                        }
                        //createOrder = true;
                    }
                    if (k == ConsoleKey.C)
                    {
                        ToggleInstrumentPosition(Client.AliceBlue, OptionType.Call);
                    }
                    if (k == ConsoleKey.P)
                    {
                        ToggleInstrumentPosition(Client.AliceBlue, OptionType.Put);
                    }
                    if (k == ConsoleKey.DownArrow)
                    {
                        WriteMsgToConsole("---> Enter no. of lots to REMOVE");
                        removeLots = DateTime.Now.TimeOfDay;
                    }
                    if (k == ConsoleKey.UpArrow)
                    {
                        WriteMsgToConsole("---> Enter no. of lots to ADD");
                        addLots = DateTime.Now.TimeOfDay;
                    }
                    if (k == ConsoleKey.W)
                    {
                        _ticker.Reconnect();
                        startingFreezeCounter = 5;
                        startingFreeze = true;
                    }
                    if (Char.IsDigit((char)k))
                    {
                        var value = int.Parse(((char)k).ToString());
                        if ((DateTime.Now.TimeOfDay - removeLots).TotalSeconds < 5)
                        {
                            WriteMsgToConsole("Remove " + value + " lots");
                            //Remove x number of lots
                        }
                        if ((DateTime.Now.TimeOfDay - addLots).TotalSeconds < 5)
                        {
                            WriteMsgToConsole("Add " + value + " lots");
                            //Add x number of lots
                        }
                    }
                    if (startingFreeze && startingFreezeCounter-- < 0)
                    {
                        startingFreeze = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            while (true)
            {
                Thread.Sleep(10);
            }
        }

        private static void _ticker_OnTick(Tick TickData)
        {
            lastUpdated = DateTime.Now.TimeOfDay;
            if (TickData == null || TickData.FeedTime == null || TickData.LastTradedPrice == 0)
            {
                Console.Write(".");
                return;
            }
            //Console.Write(".");

            if (TickData.Token == indiaVixToken)
            {
                var ltp = Convert.ToInt32(TickData.LastTradedPrice * 100);
                if (DateTime.Now.TimeOfDay <= OrderPlacementTime && string.IsNullOrEmpty(putSellOrderOpenId) && string.IsNullOrEmpty(callSellOrderOpenId))
                {
                    //lock (currentStrikeData)
                    //{
                    //    lock (currentSubscriptions)
                    //    {
                            if (DateTime.Now.TimeOfDay <= OrderPlacementTime && string.IsNullOrEmpty(putSellOrderOpenId) && string.IsNullOrEmpty(callSellOrderOpenId))
                            {
                                indiaVix = ltp;
                            }
                    //    }
                    //}
                }
            }
            else if (TickData.Token == niftyBankInstrumentToken)
            {
                var ltp = Convert.ToInt32(TickData.LastTradedPrice * 100);
                var exchangeTimeStamp = (DateTime)TickData.FeedTime;
                //if (syncDataToDb)
                //{
                //    try
                //    {
                //        securityTicksQueue.Enqueue(new SecurityTick() { SecurityId = 2, Low = ltp / 100m, High = ltp / 100m, Timestamp = DateTime.Now, Source = DataSource.AliceBlue });
                //    }
                //    catch (Exception ex)
                //    {
                //        WriteMsgToConsole("Failed to enqueue NiftyBank ticker: " + ex.Message);
                //    }
                //}
                //lock (currentStrikeData)
                //{
                    if (isNiftyBankTickerClosed) return;
                    if (currentStrikeData.StrikePrice == 0)
                    {
                        currentStrikeData.StrikePrice = Helper.GetStrikePriceFromCurrentTick(ltp);
                        currentStrikeData.ExchangeTimeStamp = exchangeTimeStamp;
                        currentStrikeData.Ltp = ltp;
                    }
                    else if (currentStrikeData.ExchangeTimeStamp < exchangeTimeStamp)
                    {
                        if (currentStrikeData.StrikePrice != Helper.GetStrikePriceFromCurrentTick(ltp))
                        {
                            currentStrikeData.StrikePrice = Helper.GetStrikePriceFromCurrentTick(ltp);
                            currentStrikeData.ExchangeTimeStamp = exchangeTimeStamp;
                            currentStrikeData.Ltp = ltp;
                        }
                        else
                        {
                            currentStrikeData.ExchangeTimeStamp = exchangeTimeStamp;
                            currentStrikeData.Ltp = ltp;
                        }
                    }
                //}
            }
            else if (TickData.Token != null)
            {
                var instrumentToken = (int)TickData.Token;
                BankNiftyOption optionData = null;
                //lock (currentSubscriptions)
                //{
                    if (currentSubscriptions.Subscriptions.ContainsKey(instrumentToken))
                    {
                        optionData = currentSubscriptions.Subscriptions[instrumentToken];
                    }
                //}
                if (optionData == null) return;

                //lock (optionData)
                {
                    //var ltp = Helper.BinaryToInt32(msg.Binary, 6);
                    {
                        var buyPrice = Convert.ToInt32(TickData.BuyPrice1 * 100);
                        var sellPrice = Convert.ToInt32(TickData.SellPrice1 * 100);
                        if (buyPrice != 0 || sellPrice != 0)
                        {
                            var exchangeTimeStamp = TickData.FeedTime;
                            //optionData.Ltp = ltp;
                            optionData.ExchangeTimeStamp = (DateTime)exchangeTimeStamp;
                            lastTickerFeed = optionData.ExchangeTimeStamp.TimeOfDay;
                            if (buyPrice != 0) optionData.BuyPrice = buyPrice;
                            if (sellPrice != 0) optionData.SellPrice = sellPrice;

                            //if (syncDataToDb)
                            //{
                            //    try
                            //    {
                            //        securityTicksQueue.Enqueue(new SecurityTick()
                            //        {
                            //            SecurityId = instrumentTokenSecurityIdMapping[instrumentToken],
                            //            Low = sellPrice / 100m,
                            //            High = buyPrice / 100m,
                            //            Source = DataSource.AliceBlue,
                            //            Timestamp = DateTime.Now
                            //        });
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        WriteMsgToConsole("Failed to enqueue Option ticker: " + ex.Message);
                            //    }
                            //}
                        }
                    }
                    //WriteMsgToConsole($"{instrumentToken} - {optionData.Ltp / 100m} {optionData.BuyPrice / 100m} {optionData.SellPrice / 100m}");
                }
                if ((LowerRisk200Straddle || indiaVix >= indiaVixThreshold) && !allPositionsClosed)
                {
                    if (instrumentToken == putInstrumentToken && !string.IsNullOrEmpty(putSellOrderOpenId) && !peBuyClosed)
                    {
                        //lock (currentSubscriptions)
                        {
                            if (allPositionsClosed) return;
                            if (!peSellTriggered && optionData.SellPrice <= peSellPrice)
                            {
                                //trigger pe sell order
                                putSellOrderOpenId = SellOpenAtMarketPrice(Client.AliceBlue, instrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1);
                                WriteMsgToConsole($"PE market sell order triggered at: {optionData.SellPrice}");
                                //peSellPrice = optionData.SellPrice;
                                peSellTriggered = true;
                            }
                            if (!peBuyTriggered && optionData.BuyPrice >= peBuyPrice)
                            {
                                //trigger pe buy order
                                putBuyOrderOpenId = BuyOpenAtLimitPrice(Client.AliceBlue, instrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1 * 2, buyLimitPrice);
                                WriteMsgToConsole($"PE market buy order triggered at: {optionData.BuyPrice}");
                                //peBuyPrice = optionData.BuyPrice;
                                peBuyTriggered = true;
                            }
                            if (peBuyTriggered && !peBuyClosed && optionData.SellPrice <= peBuyClosePrice)
                            {
                                //trigger pe sell order to close buy order
                                putBuyOrderCloseId = SellOpenAtMarketPrice(Client.AliceBlue, instrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1);
                                WriteMsgToConsole($"PE market sell order triggered at: {optionData.SellPrice}");
                                //peBuyClosePrice = optionData.SellPrice;
                                peBuyClosed = true;
                            }
                            var pnl = 0;
                            if (peSellTriggered)
                            {
                                pnl += peSellPrice - optionData.BuyPrice;
                            }
                            if (peBuyTriggered)
                            {
                                if (peBuyClosed)
                                {
                                    pnl += ((peBuyClosePrice - peBuyPrice) * 2);
                                }
                                else
                                {
                                    pnl += ((optionData.SellPrice - peBuyPrice) * 2);
                                }
                            }
                            pePnL = pnl * 25;
                            if ((maxPnLCrossedThreshold && (cePnL + pePnL) <= (maxPnL - 300000)) || (cePnL + pePnL) <= -300000)
                            {
                                //Close all open positions
                                CloseAllOpenPositions(Client.AliceBlue);
                                WriteMsgToConsole("Close all open positions");
                            }
                            if ((cePnL + pePnL) > maxPnL)
                            {
                                maxPnL = cePnL + pePnL;
                                //if (maxPnL > 300000) maxPnLCrossedThreshold = true;
                                maxPnLCrossedThreshold = true;
                            }
                            if (DateTime.Now.TimeOfDay > MarketCloseTime)
                            {
                                CloseAllOpenPositions(Client.AliceBlue);
                                WriteMsgToConsole("AutoSquareOff Close all open positions");
                            }
                        }
                    }
                    if (instrumentToken == callInstrumentToken && !string.IsNullOrEmpty(callSellOrderOpenId) && !ceBuyClosed)
                    {
                        //lock (currentSubscriptions)
                        {
                            if (allPositionsClosed) return;
                            if (!ceSellTriggered && optionData.SellPrice <= ceSellPrice)
                            {
                                //trigger ce sell order
                                callSellOrderOpenId = SellOpenAtMarketPrice(Client.AliceBlue, instrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1);
                                WriteMsgToConsole($"CE market sell order triggered at: {optionData.SellPrice}");
                                ceSellTriggered = true;
                            }
                            if (!ceBuyTriggered && optionData.BuyPrice >= ceBuyPrice)
                            {
                                //trigger ce buy order
                                callBuyOrderOpenId = BuyOpenAtLimitPrice(Client.AliceBlue, instrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1 * 2, buyLimitPrice);
                                WriteMsgToConsole($"CE market buy order triggered at: {optionData.BuyPrice}");
                                ceBuyTriggered = true;
                            }
                            if (ceBuyTriggered && !ceBuyClosed && optionData.SellPrice <= ceBuyClosePrice)
                            {
                                //trigger ce sell order to close buy order
                                callBuyOrderCloseId = SellOpenAtMarketPrice(Client.AliceBlue, instrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1);
                                WriteMsgToConsole($"CE market sell order triggered at: {optionData.SellPrice}");
                                //ceBuyClosePrice = optionData.SellPrice;

                                ceBuyClosed = true;
                            }
                            var pnl = 0;
                            if (ceSellTriggered)
                            {
                                pnl += ceSellPrice - optionData.BuyPrice;
                            }
                            if (ceBuyTriggered)
                            {
                                if (ceBuyClosed)
                                {
                                    pnl += ((ceBuyClosePrice - ceBuyPrice) * 2);
                                }
                                else
                                {
                                    pnl += ((optionData.SellPrice - ceBuyPrice) * 2);
                                }
                            }
                            cePnL = pnl * 25;
                            if ((maxPnLCrossedThreshold && (cePnL + pePnL) <= (maxPnL - 300000)) || (cePnL + pePnL) <= -300000)
                            {
                                //Close all open positions
                                CloseAllOpenPositions(Client.AliceBlue);
                                WriteMsgToConsole("Close all open positions");
                            }
                            if ((cePnL + pePnL) > maxPnL)
                            {
                                maxPnL = cePnL + pePnL;
                                //if (maxPnL > 300000) maxPnLCrossedThreshold = true;
                                maxPnLCrossedThreshold = true;
                            }
                            if (DateTime.Now.TimeOfDay > MarketCloseTime)
                            {
                                CloseAllOpenPositions(Client.AliceBlue);
                                WriteMsgToConsole("AutoSquareOff Close all open positions");
                            }
                        }
                    }

                    if (!startingFreeze && DateTime.Now.TimeOfDay > OrderPlacementTime && string.IsNullOrEmpty(putSellOrderOpenId) && string.IsNullOrEmpty(callSellOrderOpenId))
                    {
                        //lock (currentSubscriptions)
                        {
                            var callDiff = 10000000;
                            var putDiff = 10000000;
                            var callToken = 0;
                            var putToken = 0;
                            foreach (var optionDataKeyVal in currentSubscriptions.Subscriptions)
                            {
                                if (optionDataKeyVal.Value.OptionType == OptionType.Call)
                                {
                                    if (Math.Abs(20000 - optionDataKeyVal.Value.SellPrice) < callDiff)
                                    {
                                        callDiff = Math.Abs(20000 - optionDataKeyVal.Value.SellPrice);
                                        callToken = optionDataKeyVal.Key;
                                    }
                                }
                                else
                                {
                                    if (Math.Abs(20000 - optionDataKeyVal.Value.SellPrice) < putDiff)
                                    {
                                        putDiff = Math.Abs(20000 - optionDataKeyVal.Value.SellPrice);
                                        putToken = optionDataKeyVal.Key;
                                    }
                                }
                            }
                            if (string.IsNullOrEmpty(putSellOrderOpenId) && (LowerRisk200Straddle || indiaVix >= indiaVixThreshold))
                            {
                                peSellPrice = (int)(Helper.NormalizeTickPrice(currentSubscriptions.Subscriptions[putToken].SellPrice - 500) * 100);
                                peBuyPrice = (int)(Helper.NormalizeTickPrice((currentSubscriptions.Subscriptions[putToken].SellPrice - 500) * 125 / 100) * 100);
                                peBuyClosePrice = (int)(Helper.NormalizeTickPrice(peBuyPrice * 75 / 100) * 100);
                                peSellTriggered = false;
                                peBuyTriggered = false;
                                putInstrumentToken = putToken;
                                WriteMsgToConsole($"PE strike: {currentSubscriptions.Subscriptions[putToken].StrikePrice} PE sell price: {peSellPrice} PE buy price: {peBuyPrice} PE buy close: {peBuyClosePrice}");
                                //putSellOrderOpenId = SellOpenAtTriggerPrice(Client.AliceBlue, putToken, Helper.NormalizeTickPrice(currentSubscriptions.Subscriptions[putToken].SellPrice - 500), bankNiftyLotSize * aliceBlueNumberOfLots1);
                                //putBuyOrderOpenId = BuyOpenAtTriggerPrice(Client.AliceBlue, putToken, Helper.NormalizeTickPrice((currentSubscriptions.Subscriptions[putToken].SellPrice - 500) * 125 / 100), bankNiftyLotSize * aliceBlueNumberOfLots1 * 2);
                                //putSellOrderOpenId = SellOpenAtTriggerPrice(Client.PayTM, putToken, Helper.NormalizeTickPrice(currentSubscriptions.Subscriptions[putToken].SellPrice - 500), bankNiftyLotSize * paytmNumberOfLots);
                                //putBuyOrderOpenId = BuyOpenAtTriggerPrice(Client.PayTM, putToken, Helper.NormalizeTickPrice((currentSubscriptions.Subscriptions[putToken].SellPrice - 500) * 125 / 100), bankNiftyLotSize * paytmNumberOfLots * 2);
                                putSellOrderOpenId = "test";
                                putBuyOrderOpenId = "test";
                            }
                            if (string.IsNullOrEmpty(callSellOrderOpenId) && (LowerRisk200Straddle || indiaVix >= indiaVixThreshold))
                            {
                                ceSellPrice = (int)(Helper.NormalizeTickPrice(currentSubscriptions.Subscriptions[callToken].SellPrice - 500) * 100);
                                ceBuyPrice = (int)(Helper.NormalizeTickPrice((currentSubscriptions.Subscriptions[callToken].SellPrice - 500) * 125 / 100) * 100);
                                ceBuyClosePrice = (int)(Helper.NormalizeTickPrice(ceBuyPrice * 75 / 100) * 100);
                                ceSellTriggered = false;
                                peBuyTriggered = false;
                                callInstrumentToken = callToken;
                                WriteMsgToConsole($"CE strike: {currentSubscriptions.Subscriptions[callToken].StrikePrice} CE sell price: {ceSellPrice} CE buy price: {ceBuyPrice} CE buy close: {ceBuyClosePrice}");
                                //callSellOrderOpenId = SellOpenAtTriggerPrice(Client.AliceBlue, callToken, Helper.NormalizeTickPrice(currentSubscriptions.Subscriptions[callToken].SellPrice - 500), bankNiftyLotSize * aliceBlueNumberOfLots1);
                                //callBuyOrderOpenId = BuyOpenAtTriggerPrice(Client.AliceBlue, callToken, Helper.NormalizeTickPrice((currentSubscriptions.Subscriptions[callToken].SellPrice - 500) * 125 / 100), bankNiftyLotSize * aliceBlueNumberOfLots1 * 2);
                                //callSellOrderOpenId = SellOpenAtTriggerPrice(Client.PayTM, callToken, Helper.NormalizeTickPrice(currentSubscriptions.Subscriptions[callToken].SellPrice - 500), bankNiftyLotSize * paytmNumberOfLots);
                                //callBuyOrderOpenId = BuyOpenAtTriggerPrice(Client.PayTM, callToken, Helper.NormalizeTickPrice((currentSubscriptions.Subscriptions[callToken].SellPrice - 500) * 125 / 100), bankNiftyLotSize * paytmNumberOfLots * 2);
                                callSellOrderOpenId = "test";
                                callBuyOrderOpenId = "test";
                            }
                        }
                    }
                }
                if (MultiStraddle && indiaVix < indiaVixThreshold && !allPositionsClosed)
                {
                    if ((instrumentToken == putInstrumentToken && !string.IsNullOrEmpty(putSellOrderOpenId))
                            || (instrumentToken == callInstrumentToken && !string.IsNullOrEmpty(callSellOrderOpenId)))
                    {
                        //lock (currentSubscriptions)
                        {
                            if (DateTime.Now.TimeOfDay > MarketCloseTime && !allPositionsClosed)
                            {
                                CloseAllOpenPositions(Client.AliceBlue);
                                WriteMsgToConsole("AutoSquareOff Close all open positions");
                                return;
                            }
                            var bankNiftyEntryLtpCache = bankNiftyEntryLtp;
                            if ((currentStrikeData.Ltp > bankNiftyEntryLtpCache * 1.01m && ceSellTriggered) || (currentStrikeData.Ltp < bankNiftyEntryLtpCache * 0.99m && peSellTriggered))
                            {
                                //lock (currentStrikeData)
                                {
                                    if (((currentStrikeData.Ltp > bankNiftyEntryLtpCache * 1.01m && ceSellTriggered) || (currentStrikeData.Ltp < bankNiftyEntryLtpCache * 0.99m && peSellTriggered))
                                            && bankNiftyEntryLtp == bankNiftyEntryLtpCache && !allPositionsClosed)
                                    {
                                        if (currentStrikeData.Ltp > bankNiftyEntryLtpCache * 1.01m && ceSellTriggered && !ceBuyTriggered)
                                        {
                                            BuyOpenAtLimitPrice(Client.AliceBlue, callInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1, ceSellPrice < ((int)(buyLimitPrice * 50)) ? buyLimitPrice : (ceSellPrice * 2 / 100.00m));
                                            WriteMsgToConsole(">>> CE sell closed at market price\n");
                                            ceSellTriggered = false;
                                            //multiStraddleTrailPnl += cePnL;
                                        }
                                        if (currentStrikeData.Ltp < bankNiftyEntryLtpCache * 0.99m && peSellTriggered && !peBuyTriggered)
                                        {
                                            BuyOpenAtLimitPrice(Client.AliceBlue, putInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1, peSellPrice < ((int)(buyLimitPrice * 50)) ? buyLimitPrice : (peSellPrice * 2 / 100.00m));
                                            WriteMsgToConsole(">>> PE sell closed at market price\n");
                                            peSellTriggered = false;
                                            //multiStraddleTrailPnl += pePnL;
                                        }
                                        //CloseAllOpenPositions(Client.AliceBlue);
                                        //allPositionsClosed = false;
                                        //WriteMsgToConsole("All open positions closed!!");
                                        //createOrder = false;

                                        //BuyOpenAtLimitPrice(Client.AliceBlue, putInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1, peSellPrice < ((int)(buyLimitPrice * 50)) ? buyLimitPrice : (peSellPrice * 2 / 100.00m));
                                        //BuyOpenAtLimitPrice(Client.AliceBlue, callInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1, ceSellPrice < ((int)(buyLimitPrice * 50)) ? buyLimitPrice : (ceSellPrice * 2 / 100.00m));
                                        //callInstrumentToken = strikeInstrumentTokenDict[currentStrikeData.StrikePrice].Item1;
                                        //putInstrumentToken = strikeInstrumentTokenDict[currentStrikeData.StrikePrice].Item2;
                                        //putSellOrderOpenId = SellOpenAtMarketPrice(Client.AliceBlue, putInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1);
                                        //callSellOrderOpenId = SellOpenAtMarketPrice(Client.AliceBlue, callInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1);
                                        //peSellTriggered = true;
                                        //ceSellTriggered = true;
                                        //peSellPrice = currentSubscriptions.Subscriptions[putInstrumentToken].SellPrice;
                                        //ceSellPrice = currentSubscriptions.Subscriptions[callInstrumentToken].SellPrice;
                                        //bankNiftyEntryLtp = currentStrikeData.Ltp;
                                        //multiStraddleTrailPnl += pePnL + cePnL;
                                    }
                                }
                            }
                            else
                            {
                                if (instrumentToken == putInstrumentToken && peSellTriggered)
                                {
                                    pePnL = (peSellPrice - optionData.BuyPrice) * bankNiftyLotSize;
                                }
                                if (instrumentToken == callInstrumentToken && ceSellTriggered)
                                {
                                    cePnL = (ceSellPrice - optionData.BuyPrice) * bankNiftyLotSize;
                                }
                                if (multiStraddleTrailPnl + pePnL + cePnL > maxPnL)
                                {
                                    maxPnL = multiStraddleTrailPnl + pePnL + cePnL;
                                }
                            }
                        }
                    }
                    if (!startingFreeze && DateTime.Now.TimeOfDay > OrderPlacementTime && string.IsNullOrEmpty(putSellOrderOpenId) && string.IsNullOrEmpty(callSellOrderOpenId))
                    {
                        //lock (currentStrikeData)
                        {
                            //lock (currentSubscriptions)
                            {
                                if (string.IsNullOrEmpty(putSellOrderOpenId) && string.IsNullOrEmpty(callSellOrderOpenId) && MultiStraddle && indiaVix < indiaVixThreshold)
                                {
                                    callInstrumentToken = strikeInstrumentTokenDict[currentStrikeData.StrikePrice].Item1;
                                    putInstrumentToken = strikeInstrumentTokenDict[currentStrikeData.StrikePrice].Item2;
                                    putSellOrderOpenId = SellOpenAtMarketPrice(Client.AliceBlue, putInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1);
                                    callSellOrderOpenId = SellOpenAtMarketPrice(Client.AliceBlue, callInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1);
                                    peSellTriggered = true;
                                    ceSellTriggered = true;
                                    peSellPrice = currentSubscriptions.Subscriptions[putInstrumentToken].SellPrice;
                                    ceSellPrice = currentSubscriptions.Subscriptions[callInstrumentToken].SellPrice;
                                    bankNiftyEntryLtp = currentStrikeData.Ltp;
                                    WriteMsgToConsole("BNF price: " + bankNiftyEntryLtp / 100m);
                                }
                            }
                        }
                    }
                }
            }



            //var writeDone = false;
            //while (!writeDone)
            //{
            //    try
            //    {
            //        //var token = Helper.BinaryToInt32(msg.Binary, 2).ToString();
            //        //var ltp = Helper.BinaryToInt32(msg.Binary, 6) / 100.00m;
            //        //if (currentTicker.High == 0.00m) currentTicker.Open = ltp;
            //        //if (currentTicker.High < ltp) currentTicker.High = ltp;
            //        //if (currentTicker.Low > ltp) currentTicker.Low = ltp;
            //        //currentTicker.Close = ltp;
            //        //StreamWriter writetext = new StreamWriter("C:\\Users\\gvenkat8\\OneDrive - UHG\\New\\Gattu\\Personal\\Projects\\Data\\AliceBlue\\" + token + "-" + currentHourEpoch + suffix + ".txt", true);
            //        //writetext.WriteLine($"{DateTime.Now} TS:{latestTimeStamp} LTP:{Helper.BinaryToInt32(msg.Binary, 6) / 100.00m} " +
            //        //    $"LTT:{Helper.BinaryToInt32(msg.Binary, 10)} LTQ:{Helper.BinaryToInt32(msg.Binary, 14)} " +
            //        //    $"TV:{Helper.BinaryToInt32(msg.Binary, 18)} TBQ:{Helper.BinaryToInt64(msg.Binary, 38)} " +
            //        //    $"TSQ:{Helper.BinaryToInt64(msg.Binary, 46)} ATP:{Helper.BinaryToInt32(msg.Binary, 54) / 100.00m} " +
            //        //    $"BBP:{Helper.BinaryToInt32(msg.Binary, 22) / 100.00m} BBQ:{Helper.BinaryToInt32(msg.Binary, 26)} " +
            //        //    $"BAP:{Helper.BinaryToInt32(msg.Binary, 30) / 100.00m} BAQ:{Helper.BinaryToInt32(msg.Binary, 34)} " +
            //        //    $"ET:{Helper.BinaryToInt32(msg.Binary, 58)} O:{Helper.BinaryToInt32(msg.Binary, 62) / 100.00m} " +
            //        //    $"H:{Helper.BinaryToInt32(msg.Binary, 66) / 100.00m} L:{Helper.BinaryToInt32(msg.Binary, 70) / 100.00m} " +
            //        //    $"C:{Helper.BinaryToInt32(msg.Binary, 74) / 100.00m}");
            //        //writetext.Dispose();
            //        writeDone = true;
            //    }
            //    catch (Exception)
            //    {
            //        //suffix += "a";
            //        continue;
            //    }
            //}
        }

        private static void _ticker_OnReconnect()
        {
            Console.WriteLine("Ticker reconnecting.");
        }

        private static void _ticker_OnNoReconnect()
        {
            Console.WriteLine("Ticker not reconnected.");
        }

        private static void _ticker_OnError(string Message)
        {
            Console.WriteLine("Ticker error." + Message);
            _ticker.Reconnect();
            startingFreeze = true;
            startingFreezeCounter = 5;
        }

        private static void _ticker_OnClose()
        {
            Console.WriteLine("Ticker closed.");
        }

        private static void _ticker_OnConnect()
        {
            Console.WriteLine("Ticker connected.");

            Thread.Sleep(2000);
            //_ticker.Subscribe(Constants.TICK_MODE_FULL,
            //    new SubscriptionToken[]
            //        {
            //           new SubscriptionToken
            //           {
            //               Exchange = Constants.EXCHANGE_NSE,
            //               Token = 26000
            //           },
            //           new SubscriptionToken
            //           {
            //               Exchange = Constants.EXCHANGE_NSE,
            //               Token = 26009
            //           },
            //           new SubscriptionToken
            //           {
            //               Exchange = Constants.EXCHANGE_NFO,
            //               Token = 35042
            //           },
            //        });

            //_ticker.Subscribe(Constants.EXCHANGE_NSE, Constants.TICK_MODE_FULL, new int[] { 26009 });
        }

        private static void WriteMsgToConsole(string message)
        {
            Console.WriteLine(message);
            //if (syncLogsToServiceBus)
            //{
            //    var serviceBusMessage = new ServiceBusMessage(message);
            //    sender.SendMessageAsync(serviceBusMessage);
            //}
        }
        public static void ToggleInstrumentPosition(Client client, OptionType optionType)
        {
            var msg = string.Empty;
            if (client == Client.AliceBlue)
            {
                if (optionType == OptionType.Call)
                {
                    if (createCeOrder)
                    {
                        if (ceSellTriggered && !ceBuyTriggered)
                        {
                            BuyOpenAtLimitPrice(Client.AliceBlue, callInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1, ceSellPrice < ((int)(buyLimitPrice * 50)) ? buyLimitPrice : (ceSellPrice * 2 / 100.00m));
                            msg += ">>> CE sell closed at market price\n";
                        }
                        if (ceBuyTriggered && !ceBuyClosed)
                        {
                            SellOpenAtMarketPrice(Client.AliceBlue, callInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1 * (ceSellTriggered ? 1 : 2));
                            msg += ">>> CE buy closed at market price \n";
                        }
                        createCeOrder = false;
                    }
                    else
                    {
                        createCeOrder = true;
                        if (ceSellTriggered && !ceBuyTriggered)
                        {
                            SellOpenAtMarketPrice(Client.AliceBlue, callInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1);
                            msg += ">>> CE sell opened at market price\n";
                        }
                        if (ceBuyTriggered && !ceBuyClosed)
                        {
                            BuyOpenAtLimitPrice(Client.AliceBlue, callInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1 * (ceSellTriggered ? 1 : 2), ceSellPrice < ((int)(buyLimitPrice * 50)) ? buyLimitPrice : (ceSellPrice * 2 / 100.00m));
                            msg += ">>> CE buy opened at market price \n";
                        }
                    }
                }
                else
                {
                    if (createPeOrder)
                    {
                        if (peSellTriggered && !peBuyTriggered)
                        {
                            BuyOpenAtLimitPrice(Client.AliceBlue, putInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1, peSellPrice < ((int)(buyLimitPrice * 50)) ? buyLimitPrice : (peSellPrice * 2 / 100.00m));
                            msg += ">>> PE sell closed at market price\n";
                        }
                        if (peBuyTriggered && !peBuyClosed)
                        {
                            SellOpenAtMarketPrice(Client.AliceBlue, putInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1 * (peSellTriggered ? 1 : 2));
                            msg += ">>> PE buy closed at market price\n";
                        }
                        createPeOrder = false;
                    }
                    else
                    {
                        createPeOrder = true;
                        if (peSellTriggered && !peBuyTriggered)
                        {
                            SellOpenAtMarketPrice(Client.AliceBlue, putInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1);
                            msg += ">>> PE sell opened at market price\n";
                        }
                        if (peBuyTriggered && !peBuyClosed)
                        {
                            BuyOpenAtLimitPrice(Client.AliceBlue, putInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1 * (peSellTriggered ? 1 : 2), peSellPrice < ((int)(buyLimitPrice * 50)) ? buyLimitPrice : (peSellPrice * 2 / 100.00m));
                            msg += ">>> PE buy opened at market price\n";
                        }
                    }
                }
            }
            WriteMsgToConsole(msg);
        }

        public static void CloseAllOpenPositions(Client client)
        {
            if (allPositionsClosed) return;
            allPositionsClosed = true;
            var msg = string.Empty;
            if (client == Client.AliceBlue)
            {
                if (peSellTriggered && !peBuyTriggered)
                {
                    BuyOpenAtLimitPrice(Client.AliceBlue, putInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1, peSellPrice < ((int)(buyLimitPrice * 50)) ? buyLimitPrice : (peSellPrice * 2 / 100.00m));
                    msg += ">>> PE sell closed at market price\n";
                }
                if (peBuyTriggered && !peBuyClosed)
                {
                    SellOpenAtMarketPrice(Client.AliceBlue, putInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1 * (peSellTriggered ? 1 : 2));
                    msg += ">>> PE buy closed at market price\n";
                }
                if (ceSellTriggered && !ceBuyTriggered)
                {
                    BuyOpenAtLimitPrice(Client.AliceBlue, callInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1, ceSellPrice < ((int)(buyLimitPrice * 50)) ? buyLimitPrice : (ceSellPrice * 2 / 100.00m));
                    msg += ">>> CE sell closed at market price\n";
                }
                if (ceBuyTriggered && !ceBuyClosed)
                {
                    SellOpenAtMarketPrice(Client.AliceBlue, callInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1 * (ceSellTriggered ? 1 : 2));
                    msg += ">>> CE buy closed at market price \n";
                }
                WriteMsgToConsole(msg);
            }
        }

        public static void ReopenClosePositions(Client client)
        {
            var msg = string.Empty;
            if (client == Client.AliceBlue)
            {
                if (peSellTriggered && !peBuyTriggered)
                {
                    SellOpenAtMarketPrice(Client.AliceBlue, putInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1);
                    msg += ">>> PE sell opened at market price\n";
                }
                if (peBuyTriggered && !peBuyClosed)
                {
                    BuyOpenAtLimitPrice(Client.AliceBlue, putInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1 * (peSellTriggered ? 1 : 2), peSellPrice < ((int)(buyLimitPrice * 50)) ? buyLimitPrice : (peSellPrice * 2 / 100.00m));
                    msg += ">>> PE buy opened at market price\n";
                }
                if (ceSellTriggered && !ceBuyTriggered)
                {
                    SellOpenAtMarketPrice(Client.AliceBlue, callInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1);
                    msg += ">>> CE sell opened at market price\n";
                }
                if (ceBuyTriggered && !ceBuyClosed)
                {
                    BuyOpenAtLimitPrice(Client.AliceBlue, callInstrumentToken, bankNiftyLotSize * aliceBlueNumberOfLots1 * (ceSellTriggered ? 1 : 2), ceSellPrice < ((int)(buyLimitPrice * 50)) ? buyLimitPrice : (ceSellPrice * 2 / 100.00m));
                    msg += ">>> CE buy opened at market price \n";
                }
                WriteMsgToConsole(msg);
            }
        }

        //public static string SellOpenAtTriggerPrice(Client client, int instrumentToken, decimal triggerPrice, int quantity)
        //{
        //    var isCe = ceInstrumentList.Contains(instrumentToken);
        //    if (createOrder && createRealOrder && ((isCe && createCeOrder) || (!isCe && createPeOrder)))
        //    {
        //        if (client == Client.AliceBlue)
        //        {
        //            var omsOrderId = aliceBlueGateway.PlaceOrder(new AliceBlueOrderRequestModel
        //            {
        //                DisclosedQuantity = 0,
        //                Exchange = Exchange.NFO.ToString(),
        //                InstrumentToken = instrumentToken,
        //                OrderTag = orderTag,
        //                OrderType = Helper.OrderTypeValue[OrderType.SLM],
        //                Price = 0,
        //                Product = Product.MIS.ToString(),
        //                Quantity = quantity,
        //                Source = Source.web.ToString(),
        //                TransactionType = TransactionType.SELL.ToString(),
        //                TriggerPrice = triggerPrice,
        //                Validity = Validity.DAY.ToString()
        //            });
        //            return omsOrderId;
        //        }
        //        else if (client == Client.PayTM)
        //        {
        //            var orderId = paytmMoneyApiGateway.PlaceOrder(new PaytmOrderRequestModel
        //            {
        //                Exchange = "NSE",
        //                OffMktFlag = false,
        //                OrderType = "SLM",
        //                Price = 0,
        //                Product = "I",
        //                Quantity = quantity,
        //                SecurityId = instrumentToken,
        //                Segment = "D",
        //                Source = "W",
        //                TriggerPrice = triggerPrice.ToString(),
        //                TxnType = "S",
        //                Validity = "DAY"
        //            });
        //            return orderId;
        //        }
        //    }
        //    else
        //    {
        //        WriteMsgToConsole($"SELL: Instrument token: {instrumentToken}, trigger price: {triggerPrice}, quantity: {quantity}");
        //        if (!createRealOrder && createOrder && ((isCe && createCeOrder) || (!isCe && createPeOrder)))
        //            WriteMsgToConsole("Order blocked from sending to market.");
        //    }
        //    return "test";
        //}

        //public static string BuyOpenAtTriggerPrice(Client client, int instrumentToken, decimal triggerPrice, int quantity)
        //{
        //    var isCe = ceInstrumentList.Contains(instrumentToken);
        //    if (createOrder && createRealOrder && ((isCe && createCeOrder) || (!isCe && createPeOrder)))
        //    {
        //        if (client == Client.AliceBlue)
        //        {
        //            var omsOrderId = aliceBlueGateway.PlaceOrder(new AliceBlueOrderRequestModel
        //            {
        //                DisclosedQuantity = 0,
        //                Exchange = Exchange.NFO.ToString(),
        //                InstrumentToken = instrumentToken,
        //                OrderTag = orderTag,
        //                OrderType = Helper.OrderTypeValue[OrderType.SLM],
        //                Price = 0,
        //                Product = Product.MIS.ToString(),
        //                Quantity = quantity,
        //                Source = Source.web.ToString(),
        //                TransactionType = TransactionType.BUY.ToString(),
        //                TriggerPrice = triggerPrice,
        //                Validity = Validity.DAY.ToString()
        //            });
        //            return omsOrderId;
        //        }
        //        else if (client == Client.PayTM)
        //        {
        //            var orderId = paytmMoneyApiGateway.PlaceOrder(new PaytmOrderRequestModel
        //            {
        //                Exchange = "NSE",
        //                OffMktFlag = false,
        //                OrderType = "SLM",
        //                Price = 0,
        //                Product = "I",
        //                Quantity = quantity,
        //                SecurityId = instrumentToken,
        //                Segment = "D",
        //                Source = "W",
        //                TriggerPrice = triggerPrice.ToString(),
        //                TxnType = "B",
        //                Validity = "DAY"
        //            });
        //            return orderId;
        //        }
        //    }
        //    else
        //    {
        //        WriteMsgToConsole($"BUY: Instrument token: {instrumentToken}, trigger price: {triggerPrice}, quantity: {quantity}");
        //        if (!createRealOrder && createOrder && ((isCe && createCeOrder) || (!isCe && createPeOrder)))
        //            WriteMsgToConsole("Order blocked from sending to market.");
        //    }
        //    return "test";
        //}

        public static string SellOpenAtMarketPrice(Client client, int instrumentToken, int quantity)
        {
            var isCe = ceInstrumentList.Contains(instrumentToken);
            if (createOrder && createRealOrder && ((isCe && createCeOrder) || (!isCe && createPeOrder)))
            {
                if (client == Client.AliceBlue)
                {
                    var placeRegularOrderResult = _aliceBlue.PlaceOrder(new PlaceRegularOrderParams
                    {
                        Exchange = Constants.EXCHANGE_NFO,
                        OrderTag = orderTag,
                        PriceType = Constants.PRICE_TYPE_MARKET,
                        ProductCode = Constants.PRODUCT_CODE_MIS,
                        Quantity = quantity,
                        TransactionType = Constants.TRANSACTION_TYPE_SELL,
                        InstrumentToken = instrumentToken,
                        TradingSymbol = instrumentDict[instrumentToken]
                    });
                    return placeRegularOrderResult.OrderNumber;
                }
                else if (client == Client.PayTM)
                {
                    //var orderId = paytmMoneyApiGateway.PlaceOrder(new PaytmOrderRequestModel
                    //{
                    //    Exchange = "NSE",
                    //    OffMktFlag = false,
                    //    OrderType = "SLM",
                    //    Price = 0,
                    //    Product = "I",
                    //    Quantity = quantity,
                    //    SecurityId = instrumentToken,
                    //    Segment = "D",
                    //    Source = "W",
                    //    TriggerPrice = triggerPrice.ToString(),
                    //    TxnType = "S",
                    //    Validity = "DAY"
                    //});
                    //return orderId;
                }
            }
            else
            {
                WriteMsgToConsole($"SELL: Instrument token: {instrumentToken}, quantity: {quantity}, market order");
                if (!createRealOrder && createOrder && ((isCe && createCeOrder) || (!isCe && createPeOrder)))
                    WriteMsgToConsole("Order blocked from sending to market.");
            }
            return "test";
        }

        public static string BuyOpenAtMarketPrice(Client client, int instrumentToken, int quantity)
        {
            var isCe = ceInstrumentList.Contains(instrumentToken);
            if (createOrder && createRealOrder && ((isCe && createCeOrder) || (!isCe && createPeOrder)))
            {
                if (client == Client.AliceBlue)
                {
                    var placeRegularOrderResult = _aliceBlue.PlaceOrder(new PlaceRegularOrderParams
                    {
                        Exchange = Constants.EXCHANGE_NFO,
                        OrderTag = orderTag,
                        PriceType = Constants.PRICE_TYPE_MARKET,
                        ProductCode = Constants.PRODUCT_CODE_MIS,
                        Quantity = quantity,
                        TransactionType = Constants.TRANSACTION_TYPE_BUY,
                        InstrumentToken = instrumentToken,
                        TradingSymbol = instrumentDict[instrumentToken]
                    });
                    return placeRegularOrderResult.OrderNumber;
                }
                else if (client == Client.PayTM)
                {
                    //var orderId = paytmMoneyApiGateway.PlaceOrder(new PaytmOrderRequestModel
                    //{
                    //    Exchange = "NSE",
                    //    OffMktFlag = false,
                    //    OrderType = "SLM",
                    //    Price = 0,
                    //    Product = "I",
                    //    Quantity = quantity,
                    //    SecurityId = instrumentToken,
                    //    Segment = "D",
                    //    Source = "W",
                    //    TriggerPrice = triggerPrice.ToString(),
                    //    TxnType = "B",
                    //    Validity = "DAY"
                    //});
                    //return orderId;
                }
            }
            else
            {
                WriteMsgToConsole($"BUY: Instrument token: {instrumentToken}, quantity: {quantity}, market order");
                if (!createRealOrder && createOrder && ((isCe && createCeOrder) || (!isCe && createPeOrder)))
                    WriteMsgToConsole("Order blocked from sending to market.");
            }
            return "test";
        }

        public static string BuyOpenAtLimitPrice(Client client, int instrumentToken, int quantity, decimal limitPrice)
        {
            var isCe = ceInstrumentList.Contains(instrumentToken);
            if (createOrder && createRealOrder && ((isCe && createCeOrder) || (!isCe && createPeOrder)))
            {
                if (client == Client.AliceBlue)
                {
                    var placeRegularOrderResult = _aliceBlue.PlaceOrder(new PlaceRegularOrderParams
                    {
                        Exchange = Constants.EXCHANGE_NFO,
                        OrderTag = orderTag,
                        PriceType = Constants.PRICE_TYPE_MARKET,
                        ProductCode = Constants.PRODUCT_CODE_MIS,
                        Quantity = quantity,
                        TransactionType = Constants.TRANSACTION_TYPE_BUY,
                        InstrumentToken = instrumentToken,
                        TradingSymbol = instrumentDict[instrumentToken]
                    });
                    return placeRegularOrderResult.OrderNumber;
                }
                else if (client == Client.PayTM)
                {
                    //var orderId = paytmMoneyApiGateway.PlaceOrder(new PaytmOrderRequestModel
                    //{
                    //    Exchange = "NSE",
                    //    OffMktFlag = false,
                    //    OrderType = "SLM",
                    //    Price = 0,
                    //    Product = "I",
                    //    Quantity = quantity,
                    //    SecurityId = instrumentToken,
                    //    Segment = "D",
                    //    Source = "W",
                    //    TriggerPrice = triggerPrice.ToString(),
                    //    TxnType = "B",
                    //    Validity = "DAY"
                    //});
                    //return orderId;
                }
            }
            else
            {
                WriteMsgToConsole($"BUY: Instrument token: {instrumentToken}, quantity: {quantity}, limit price: {limitPrice}");
                if (!createRealOrder && createOrder && ((isCe && createCeOrder) || (!isCe && createPeOrder)))
                    WriteMsgToConsole("Order blocked from sending to market.");
            }
            return "test";
        }
    }
    public class BankNiftyOption
    {
        private int _sellPrice;
        public int InstrumentToken { get; set; }
        public int Ltp { get; set; }
        public int BuyPrice { get; set; }
        public int SellPrice
        {
            get { return _sellPrice; }
            set
            {
                _sellPrice = value;
                var currentMinuteTimeSpan = CurrentMinuteTimeSpan;
                if (!SellPriceHistory.ContainsKey(currentMinuteTimeSpan))
                {
                    SellPriceHistory.Add(currentMinuteTimeSpan, _sellPrice);
                }
                else
                {
                    SellPriceHistory[currentMinuteTimeSpan] = _sellPrice;
                }
            }
        }
        public DateTime ExchangeTimeStamp { get; set; }
        public TimeSpan CurrentMinuteTimeSpan
        {
            get
            {
                return new TimeSpan(ExchangeTimeStamp.Hour, ExchangeTimeStamp.Minute, 0);
            }
        }
        public OptionType OptionType { get; set; }
        public decimal StrikePrice { get; set; }
        public Dictionary<TimeSpan, int> SellPriceHistory = new();
    }

    public class BankNiftyStrikePrice
    {
        public int Ltp { get; set; }
        public int StrikePrice { get; set; }
        public DateTime ExchangeTimeStamp { get; set; }
    }

    public class SubscriptionInstruments
    {
        public Dictionary<int, BankNiftyOption> Subscriptions = new();
        public void Add(int instrumentToken, OptionType optionType, decimal strikePrice)
        {
            if (!Subscriptions.ContainsKey(instrumentToken))
            {
                Subscriptions.Add(instrumentToken, new BankNiftyOption() { InstrumentToken = instrumentToken, OptionType = optionType, StrikePrice = strikePrice });
                DevTest._ticker.Subscribe(Constants.EXCHANGE_NFO, Constants.TICK_MODE_FULL, new int[] { instrumentToken });
            }
        }
        public void Remove(int instrumentToken)
        {
            if (Subscriptions.ContainsKey(instrumentToken))
            {
                Subscriptions.Remove(instrumentToken);
                DevTest._ticker.UnSubscribe(Constants.EXCHANGE_NFO, new int[] { instrumentToken });
            }
        }
    }

    public enum AliceBlueMode
    {
        marketdata = 1,
        compact_marketdata = 2,
        snapquote = 3,
        full_snapquote = 4,
        spreaddata = 5,
        spread_snapquote = 6,
        dpr = 7,
        oi = 8,
        market_status = 9,
        exchange_messages = 10
    }

    public enum Exchange
    {
        NSE,
        BSE,
        MCX,
        NFO,
        CDS
    }

    public enum OrderType
    {
        MARKET,
        LIMIT,
        SL,
        SLM
    }

    public enum Validity
    {
        DAY,
        IOC
    }

    public enum Product
    {
        CNC,
        MIS,
        NRML,
        CO,
        BO
    }

    public enum Source
    {
        web,
        mob
    }

    public enum TransactionType
    {
        BUY,
        SELL
    }

    public enum OptionType
    {
        Put,
        Call
    }

    public enum Client
    {
        PayTM,
        AliceBlue
    }
    public static class Helper
    {
        public static Dictionary<OrderType, string> OrderTypeValue = new Dictionary<OrderType, string>
        {
            { OrderType.MARKET, "MARKET" },
            { OrderType.LIMIT, "LIMIT" },
            { OrderType.SL, "SL" },
            { OrderType.SLM, "SL-M" }
        };

        public static int DateTimeToEpoch(DateTime datetime)
        {
            return (int)(datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        public static int GetStrikePriceFromCurrentTick(int currentTick)
        {
            return (int)Math.Round(currentTick / 10000.00m, 0) * 100;
        }

        public static decimal NormalizeTickPrice(int price)
        {
            var onesPlaceValue = price % 10;
            if (onesPlaceValue < 3)
            {
                return (price - onesPlaceValue) / 100m;
            }
            else if (onesPlaceValue < 8)
            {
                return (price - onesPlaceValue + 5) / 100m;
            }
            else
            {
                return (price - onesPlaceValue + 10) / 100m;
            }
        }
    }
}
