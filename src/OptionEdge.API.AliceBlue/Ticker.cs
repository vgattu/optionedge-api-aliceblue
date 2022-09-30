﻿using OptionEdge.API.AliceBlue.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Utf8Json;

namespace OptionEdge.API.AliceBlue
{
    public class Ticker
    {
        private bool _debug = false;

        private string _userId;
        private string _accessToken;
        
        private string _socketUrl = "wss://ws1.aliceblueonline.com/NorenWS";
        private bool _isReconnect = false;
        private int _interval = 5;
        private int _retries = 50;
        private int _retryCount = 0;

        System.Timers.Timer _timer;
        int _timerTick = 5;

        private IWebSocket _ws;

        /// <summary>
        /// Token -> Mode Mapping
        /// </summary>
        private Dictionary<SubscriptionToken, string> _subscribedTokens;

        public delegate void OnConnectHandler();
        public delegate void OnCloseHandler();
        public delegate void OnTickHandler(Tick TickData);
        public delegate void OnErrorHandler(string Message);
        public delegate void OnReconnectHandler();
        public delegate void OnNoReconnectHandler();
        public event OnConnectHandler OnConnect;
        public event OnCloseHandler OnClose;
        public event OnTickHandler OnTick;
        public event OnErrorHandler OnError;
        public event OnReconnectHandler OnReconnect;
        public event OnNoReconnectHandler OnNoReconnect;

        public Ticker(string userId, string accessToken, string socketUrl = null, bool reconnect = false, int reconnectInterval = 5, int reconnectTries = 50, bool debug = false)
        {
            _debug = debug;
            _userId = userId;
            _accessToken = accessToken;
            _subscribedTokens = new Dictionary<SubscriptionToken, string>();
            _interval = reconnectInterval;
            _timerTick = reconnectInterval;
            _retries = reconnectTries;
            _isReconnect = reconnect;

            if (string.IsNullOrEmpty(socketUrl))
                _socketUrl = "wss://ws1.aliceblueonline.com/NorenWS";
            else
                _socketUrl = socketUrl;

            _ws = new WebSocket();

            _ws.OnConnect += _onConnect;
            _ws.OnData += _onData;
            _ws.OnClose += _onClose;
            _ws.OnError += _onError;

            _timer = new System.Timers.Timer();
            _timer.Elapsed += _onTimerTick;
            _timer.Interval = 1000;
        }

        private void _onError(string Message)
        {
            OnError?.Invoke(Message);
        }

        private void _onClose()
        {
            _timer.Stop();
            OnClose?.Invoke();
        }
       
        private void _onData(byte[] Data, int Count, string MessageType)
        {
            _timerTick = _interval;

            if (MessageType == "Text")
            {
                string message = Encoding.UTF8.GetString(Data.Take(Count).ToArray());
                //if (_debug) Utils.LogMessage("WebSocket Message: " + message);

                var data = JsonSerializer.Deserialize<dynamic>(Data);
                if (data["t"] == "ck")
                {
                    if (_debug)
                        Utils.LogMessage("Connection acknowledgement received. Websocket connected.");
                }
                else if (data["t"] == "tk" || data["t"] == "tf" || data["t"] == "dk" || data["t"] == "df")
                {
                    //if (data == null || data.FeedTime == null || data.LastTradedPrice == 0)
                    //{
                    //    return;
                    //}
                    if ((data["t"] == "tf" || data["t"] == "df") && Utils.IsPropertyExist(data, "ft") && Utils.IsPropertyExist(data, "lp"))
                    {
                        //ThreadPool.QueueUserWorkItem(
                        //new WaitCallback(delegate (object state)
                        //{
                        //    OnTick((Tick)state);
                        //}), new Tick(data));
                        OnTick(new Tick(data));
                    }
                } else
                {
                    if (_debug)
                        Utils.LogMessage($"Unknown feed type: {data["t"]}");
                }
            }
            else if (MessageType == "Close")
            {
                Close();
            }
        }

        private void _onTimerTick(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timerTick--;
            if (_timerTick < 0)
            {
                _timer.Stop();
                if (_isReconnect)
                    Reconnect();
            }
            //if (_debug) Utils.LogMessage(_timerTick.ToString());
        }

        private void _onConnect()
        {
            _ws.Send(JsonSerializer.ToJsonString(new CreateWebsocketConnectionRequest
            {
                AccessToken = _accessToken,
                AccountId = _userId + "_API",
                UserId = _userId + "_API"
            }));

            _retryCount = 0;
            _timerTick = _interval;
            _timer.Start();
            if (_subscribedTokens.Count > 0)
                ReSubscribe();
            OnConnect?.Invoke();
        }

        public bool IsConnected
        {
            get { return _ws.IsConnected(); }
        }

        public void Connect()
        {
            _timerTick = _interval;
            _timer.Start();
            if (!IsConnected)
            {
                _ws.Connect(_socketUrl);
            }
        }

        public void Close()
        {
            _timer.Stop();
            _ws.Close();
        }

        public void Reconnect()
        {
            if (IsConnected)
            {
                _timerTick = (int)Math.Min(Math.Pow(2, _retryCount) * _interval, 60);
                _timer.Start();
                return;
            }

            if (_retryCount > _retries)
            {
                _ws.Close(true);
                DisableReconnect();
                OnNoReconnect?.Invoke();
            }
            else
            {
                Console.WriteLine("Trying to reconnect ticker.");
                OnReconnect?.Invoke();
                _retryCount += 1;
                _ws.Close(true);
                Connect();
                _timerTick = (int)Math.Min(Math.Pow(2, _retryCount) * _interval, 60);
                if (_debug) Utils.LogMessage("New interval " + _timerTick);
                _timer.Start();
            }
        }

        public void Subscribe(string exchnage, string mode, int[] tokens)
        {
            var subscriptionTokens = tokens.Select(token => new SubscriptionToken
            {
                Token = token,
                Exchange = exchnage
            }).ToArray();

            Subscribe(mode, subscriptionTokens);
        }

        public void Subscribe(string mode, SubscriptionToken[] tokens)
        {
            if (tokens.Length == 0) return;

            var subscriptionRequst = new SubscribeFeedDataRequest
            {
                SubscriptionTokens = tokens,
                RequestType = mode == Constants.TICK_MODE_QUOTE ? Constants.SUBSCRIBE_SOCKET_TICK_DATA_REQUEST_TYPE_MARKET : Constants.SUBSCRIBE_SOCKET_TICK_DATA_REQUEST_TYPE_DEPTH,
            };

            var requestJson = JsonSerializer.ToJsonString(subscriptionRequst);

            if (_debug) Utils.LogMessage(requestJson.Length.ToString());

            if (IsConnected)
                _ws.Send(requestJson);

            foreach (SubscriptionToken token in tokens)
            {
                if (_subscribedTokens.ContainsKey(token))
                    _subscribedTokens[token] = mode; 
                else
                    _subscribedTokens.Add(token, mode);
            }
        }

        public void UnSubscribe(string exchnage, int[] tokens)
        {
            var subscriptionTokens = tokens.Select(token => new SubscriptionToken
            {
                Token = token,
                Exchange = exchnage
            }).ToArray();

            UnSubscribe(subscriptionTokens);
        }

        public void UnSubscribe(SubscriptionToken[] tokens)
        {
            if (tokens.Length == 0) return;

            var request = new UnsubscribeMarketDataRequest
            {
                SubscribedTokens = tokens,
            };

            var requestJson = JsonSerializer.ToJsonString(request);

            if (_debug) Utils.LogMessage(requestJson.Length.ToString());

            if (IsConnected)
                _ws.Send(requestJson);

            foreach (SubscriptionToken token in tokens)
                if (_subscribedTokens.ContainsKey(token))
                    _subscribedTokens.Remove(token);
        }

        private void ReSubscribe()
        {
            if (_debug) Utils.LogMessage("Resubscribing");

            SubscriptionToken[] allTokens = _subscribedTokens.Keys.ToArray();

            SubscriptionToken[] quoteTokens = allTokens.Where(key => _subscribedTokens[key] == Constants.TICK_MODE_QUOTE).ToArray();
            SubscriptionToken[] fullTokens = allTokens.Where(key => _subscribedTokens[key] == Constants.TICK_MODE_FULL).ToArray();

            UnSubscribe(quoteTokens);
            UnSubscribe(fullTokens);

            Subscribe(Constants.TICK_MODE_QUOTE, quoteTokens);
            Subscribe(Constants.TICK_MODE_FULL, fullTokens);
        }

        public void EnableReconnect(int interval = 5, int retries = 50)
        {
            _isReconnect = true;
            _interval = Math.Max(interval, 5);
            _retries = retries;

            _timerTick = _interval;
            if (IsConnected)
                _timer.Start();
        }

        public void DisableReconnect()
        {
            _isReconnect = false;
            if (IsConnected)
                _timer.Stop();
            _timerTick = _interval;
        }
    }
}
