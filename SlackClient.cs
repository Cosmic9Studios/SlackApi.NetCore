using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PureWebSockets;
using RestSharp;
using SlackApi;
using SlackApi.Events;
using SlackApi.Methods;
using SlackApi.Responses;

namespace SlackApi
{
    public class SlackClient : ISlackClient
    {
        #region Fields. 
        ///
        //////////////////////////////////
        private RestClient restClient;
        private PureWebSocket slackSocket;
        private Dictionary<string, KeyValuePair<Type, Delegate>> eventCallbacks;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SlackClient" /> class.
        /// </summary>
        public SlackClient(string token)
        {
            Token = token;
            restClient = new RestClient("https://slack.com/api");
            eventCallbacks = new Dictionary<string, KeyValuePair<Type, Delegate>>();
        }
        #endregion

        #region Public Properties.
        /// <summary>
        /// Gets and sets the token used by methods for authentication.
        /// </summary>
        public string Token { get; set; }
        #endregion

        #region ISlackClient Interface.
        /// <summary>
        /// Connects to the slack client.
        /// </summary>
        /// <param name="token">The token used to identify the user.</param>
        public async Task<ConnectResponse> Connect()
        {
            var connectionResponse = await CallApiMethod<ConnectResponse>(new RtmConnectMethod());
            if (connectionResponse.Ok)
            {
                slackSocket = new PureWebSocket(connectionResponse.Url, new ReconnectStrategy(10000, 60000, 10));
                slackSocket.OnStateChanged += Ws_OnStateChanged;
                slackSocket.OnMessage += Ws_OnMessage;
                slackSocket.OnClosed += Ws_OnClosed;

                slackSocket.Connect();
            }

            return connectionResponse;
        }

        /// <summary>
        /// Disconnects from the slack api.
        /// </summary>
        public void Disconnect()
        {
            slackSocket.Disconnect();
            eventCallbacks = null;
        }

        /// <summary>
        /// Calls a web api method.
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="parameters">The parameters to pass to the method.</param>
        public async Task<T> CallApiMethod<T>(Methods.Method method) where T: Response
        {
            string methodName = GetTypeIdentifier(method.GetType());

            Func<T> task = () =>
            {
                var request = new RestRequest($"{methodName}");

                if (!String.IsNullOrEmpty(Token))
                {
                    request.AddParameter("token", Token);
                }

                foreach (var parameter in method.Parameters)
                {
                    if (parameter.Key == null || parameter.Value == null)
                    {
                        continue;
                    }
                    request.AddParameter(parameter.Key, parameter.Value);
                }
                var result = restClient.Execute(request);
                
                return JsonConvert.DeserializeObject<T>(result.Content); 
            };

            var taskResponse = Task.Factory.StartNew(task).Result;

            while (taskResponse.Error == "ratelimited")
            {
                await Task.Delay(500);
                taskResponse = Task.Factory.StartNew(task).Result;
            }

            return taskResponse;
        }

        /// <summary>
        /// Binds a callback to an event.
        /// </summary>
        /// <param name="callback">The callback method to call when the event is invoked.</param>
        public void BindEvent<T>(Action<T> callback) where T : Event
        {
            var eventName = GetTypeIdentifier(typeof(T));
            eventCallbacks[eventName] = new KeyValuePair<Type, Delegate>(typeof(T), callback);
        }

        /// <summary>
        /// Unbinds a bound event.
        /// </summary>
        /// <param name="callback">The callback to unbind.</param>
        public void UnbindEvent<T>() where T : Event
        {
            var eventName = GetTypeIdentifier(typeof(T));
            eventCallbacks.Remove(eventName);
        }
        #endregion

        #region Private Methods. 
        private string GetTypeIdentifier(Type type)
        {
            bool isEvent = (type.IsSubclassOf(typeof(Event)));
            bool isMethod = (type.IsSubclassOf(typeof(Methods.Method)));

            string identity = ""; 
            List<string> split =  new List<string>(Regex.Split(type.Name, @"(?<!^)(?=[A-Z])"));

            var lastWord = split.LastOrDefault();
            if (lastWord != null && (isEvent || isMethod))
            {
                split.RemoveAt(split.Count - 1);
            }
            
            foreach (var name in split)
            {
                identity += name.ToLower() + (isEvent ? "_" : ".");
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
        }

        private void Ws_OnMessage(string message)
        {
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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{DateTime.Now} Status changed from {prevState} to {newState}");
            Console.ResetColor();
            Console.WriteLine("");
        }
        #endregion
    }
}