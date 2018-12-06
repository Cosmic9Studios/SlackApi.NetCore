using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PureWebSockets;
using RestSharp;
using SlackApi.Models;
using SlackApi.Responses;

namespace SlackApi.Clients
{
    public class SlackWebClient : ISlackWebClient
    {
        #region Fields. 
        ///
        //////////////////////////////////
        private RestClient restClient;
        private PureWebSocket slackSocket;
        private SlackUser user;
        // [TeamId:MethodName][{current time, target time}]
        private static Dictionary<string, KeyValuePair<Stopwatch, int>> blockedMethods;
        #endregion

        #region Constructor
        internal SlackWebClient() 
        { 
            blockedMethods = new Dictionary<string, KeyValuePair<Stopwatch, int>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlackWebClient" /> class.
        /// </summary>
        public SlackWebClient(SlackUser user) : this()
        {
            this.user = user;
            restClient = new RestClient("https://slack.com/api");
        }
        #endregion

        #region ISlackWebClient Interface.
        /// <summary>
        /// Calls a web api method.
        /// </summary>
        /// <param name="method">The method to call.</param>
        public async Task<T> CallApiMethod<T>(Methods.Method method) where T: Response
        {
            string methodName = GetMethodName(method.GetType());
            string blockKey = $"{user.TeamId}:{methodName}";

            if (blockedMethods.TryGetValue(blockKey, out var time))
            {
                var timeLeft = time.Value - time.Key.Elapsed.Seconds;
                if (timeLeft > 0)
                {
                    System.Console.WriteLine($"User {user.Id} on Team {user.TeamId} blocked for {timeLeft} on method {methodName}");
                    await Task.Delay(TimeSpan.FromSeconds(timeLeft));
                }
                
                blockedMethods.Remove(blockKey);
            }
        
            var request = new RestRequest($"{methodName}");

            if (!String.IsNullOrEmpty(user.Token))
            {
                request.AddParameter("token", user.Token);
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
            if ((int)result.StatusCode == 429)
            {
                var retryAfter = result.Headers.FirstOrDefault(x => x.Name == "Retry-After");
                if (!int.TryParse(retryAfter.Value.ToString(), out var retrySeconds))
                {
                    retrySeconds = 60;
                }

                blockedMethods[blockKey] = 
                    new KeyValuePair<Stopwatch, int>(Stopwatch.StartNew(), retrySeconds);
                
                return await CallApiMethod<T>(method);
            }

            var taskResponse = JsonConvert.DeserializeObject<T>(result.Content); 
            return taskResponse;
        }
        #endregion

        #region Private Methods. 
        private string GetMethodName(Type type)
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
                identity += name.ToLower() + ".";
            }

            identity = identity.Substring(0, identity.Length - 1);

            return identity;
        }
        #endregion
    }
}