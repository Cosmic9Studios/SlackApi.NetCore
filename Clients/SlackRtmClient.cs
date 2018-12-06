using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;
using PureWebSockets;
using RestSharp;
using SlackApi.Events;

namespace SlackApi.Clients
{
    public class SlackRtmClient : ISlackRtmClient
    {
        #region Fields.
        ///
        //////////////////////////////////
        private RestClient restClient;
        private PureWebSocket slackSocket;
        private Dictionary<string, KeyValuePair<Type, Delegate>> eventCallbacks;
        private bool isActive = false;
        private Timer timer;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SlackRtmClient" /> class.
        /// </summary>
        public SlackRtmClient()
        {
            eventCallbacks = new Dictionary<string, KeyValuePair<Type, Delegate>>();
        }
        #endregion

        #region ISlackRtmClient Interface.
        /// <summary>
        /// Connects to the slack client.
        /// </summary>
        /// <param name="socketUrl">The socket address to connect to.</param>
        public void Connect(string socketUrl)
        {
            slackSocket = new PureWebSocket(socketUrl, new ReconnectStrategy(10000, 60000));
            slackSocket.OnStateChanged += Ws_OnStateChanged;
            slackSocket.OnMessage += Ws_OnMessage;
            slackSocket.OnClosed += Ws_OnClosed;

            slackSocket.Connect();

            isActive = true;

            timer = new Timer(
                e => {
                    slackSocket.Send(@"{""id"": 1234, ""type"": ""ping""}");
                },
                null, 
                TimeSpan.Zero, 
                TimeSpan.FromSeconds(4)
            );
        }

        /// <summary>
        /// Disconnects from the slack rtm socket.
        /// </summary>
        public void Disconnect()
        {
            isActive = false;
            slackSocket.Disconnect();
            eventCallbacks = null;
        }

        /// <summary>
        /// Binds a callback to an event.
        /// </summary>
        /// <param name="callback">The callback method to call when the event is invoked.</param>
        public void BindEvent<T>(Action<T> callback) where T : Event
        {
            var eventName = GetEventName(typeof(T));
            eventCallbacks[eventName] = new KeyValuePair<Type, Delegate>(typeof(T), callback);
        }

        /// <summary>
        /// Unbinds a bound event.
        /// </summary>
        /// <param name="callback">The callback to unbind.</param>
        public void UnbindEvent<T>() where T : Event
        {
            var eventName = GetEventName(typeof(T));
            eventCallbacks.Remove(eventName);
        }
        #endregion

        #region Private Methods. 
        private string GetEventName(Type type)
        {
            string identity = ""; 
            List<string> split =  new List<string>(Regex.Split(type.Name, @"(?<!^)(?=[A-Z])"));

            var lastWord = split.LastOrDefault();
            if (lastWord != null)
            {
                split.RemoveAt(split.Count - 1);
            }
            
            foreach (var name in split)
            {
                identity += name.ToLower() + "_";
            }

            identity = identity.Substring(0, identity.Length - 1);

            return identity;
        }
        #endregion

        #region Event Methods.
        private void Ws_OnClosed(WebSocketCloseStatus reason)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now} Connection Closed: {reason}");
            Console.ResetColor();
            Console.WriteLine("");
            Console.ReadLine();

            if (isActive)
            {
                slackSocket.Connect();
            }
        }

        private void Ws_OnMessage(string message)
        {
            timer?.Change(TimeSpan.Zero, TimeSpan.FromSeconds(4));
            var slackEvent = JsonConvert.DeserializeObject<Event>(message); 
            if (slackEvent.Type == null)
            {
                return;
            }

            if (eventCallbacks.TryGetValue(slackEvent.Type, out var callback))
            {
                object eventData = JsonConvert.DeserializeObject(message, callback.Key);
                callback.Value.DynamicInvoke(eventData);
            }
        }

        private void Ws_OnStateChanged(WebSocketState newState, WebSocketState prevState)
        {
            /* Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{DateTime.Now} Status changed from {prevState} to {newState}");
            Console.ResetColor();
            Console.WriteLine(""); */
        }
        #endregion
    }
}