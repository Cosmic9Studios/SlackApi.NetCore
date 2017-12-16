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
        private string token;
        private PureWebSocket slackSocket;
        private Dictionary<string, KeyValuePair<Type, Delegate>> eventCallbacks;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SlackClient" /> class.
        /// </summary>
        public SlackClient()
        {
            restClient = new RestClient("https://slack.com/api");
            eventCallbacks = new Dictionary<string, KeyValuePair<Type, Delegate>>();
        }
        #endregion

        #region ISlackClient Interface.
        /// <summary>
        /// Connects to the slack client.
        /// </summary>
        /// <param name="token">The token used to identify the user.</param>
        public async Task<ConnectResponse> Connect(string token)
        {
            this.token = token;
            var connectionResponse = await CallApiMethod<ConnectResponse>(new RtmConnectMethod());
            if (connectionResponse.Ok)
            {
                slackSocket = new PureWebSocket(connectionResponse.Url);
                slackSocket.OnStateChanged += Ws_OnStateChanged;
                slackSocket.OnMessage += Ws_OnMessage;
                slackSocket.OnClosed += Ws_OnClosed;

                slackSocket.Connect();
            }

            return connectionResponse;
        }

        /// <summary>
        /// Calls a web api method.
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="parameters">The parameters to pass to the method.</param>
        public async Task<T> CallApiMethod<T>(IMethod method) where T: Response
        {
            string methodName = GetTypeIdentifier(method.GetType());

            return await Task.Run(() =>
            {
                var request = new RestRequest($"{methodName}");
                request.AddParameter("token", token);
                foreach (var parameter in method.Parameters)
                {
                    request.AddParameter(parameter.Key, parameter.Value);
                }
                var result = restClient.Execute(request);
                
                return JsonConvert.DeserializeObject<T>(result.Content); 
            });
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
        #endregion

        #region Private Methods. 
        private string GetTypeIdentifier(Type type)
        {
            string identity = ""; 
            List<string> split =  new List<string>(Regex.Split(type.Name, @"(?<!^)(?=[A-Z])"));

            var lastWord = split.LastOrDefault();
            if (lastWord != null && lastWord == type.BaseType.Name.ToString())
            {
                split.RemoveAt(split.Count - 1);
            }
            
            foreach (var name in split)
            {
                identity += name.ToLower() + (type.IsSubclassOf(typeof(Event)) ? "_" : ".");
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
            if (eventCallbacks.TryGetValue(slackEvent.type, out var callback))
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