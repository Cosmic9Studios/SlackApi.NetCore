using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using PureWebSockets;
using SlackApi.Events;

namespace SlackApi.Clients
{
    public class SlackRtmClient : ISlackRtmClient
    {
        #region Fields.
        ///
        //////////////////////////////////
        private ClientWebSocket client = new ClientWebSocket();
        private Dictionary<string, KeyValuePair<Type, Delegate>> eventCallbacks;
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

        private async Task ProcessMessage(ClientWebSocket client)
        {
            WebSocketReceiveResult result;
            var message = new ArraySegment<byte>(new byte[4096]);
            do 
            {
                result = await client.ReceiveAsync(message, CancellationToken.None);
                if (result.MessageType != WebSocketMessageType.Text)
                {
                    break;
                }

                var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
                string receivedMessage = Encoding.UTF8.GetString(messageBytes);
                
                Event slackEvent;

                try
                {
                    slackEvent = JsonConvert.DeserializeObject<Event>(receivedMessage); 
                }
                catch (JsonReaderException)
                {
                    var encodedMessage = HttpUtility.JavaScriptStringEncode(receivedMessage);
                    slackEvent = JsonConvert.DeserializeObject<Event>(receivedMessage); 
                }

                if (slackEvent.Type == null)
                {
                    return;
                }

                if (eventCallbacks.TryGetValue(slackEvent.Type, out var callback))
                {
                    object eventData = JsonConvert.DeserializeObject(receivedMessage, callback.Key);
                    callback.Value.DynamicInvoke(eventData);
                }
            } 
            while (!result.EndOfMessage);
        }
        
        #region ISlackRtmClient Interface.
        /// <summary>
        /// Connects to the slack client.
        /// </summary>
        /// <param name="socketUrl">The socket address to connect to.</param>
        public async Task Connect(string socketUrl)
        {
            await client.ConnectAsync(new Uri(socketUrl), CancellationToken.None);
            await Task.Factory.StartNew(async () => 
            {
                while (true) 
                {
                    await ProcessMessage(client);
                }
            }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        /// <summary>
        /// Disconnects from the slack rtm socket.
        /// </summary>
        public void Disconnect()
        {
            client.Dispose();
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
    }
}