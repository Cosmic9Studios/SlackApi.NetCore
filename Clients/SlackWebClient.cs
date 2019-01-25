using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PureWebSockets;
using Flurl;
using Flurl.Http;
using SlackApi.Models;
using SlackApi.Responses;
using System.Net.Http;
using System.Collections.Concurrent;
using System.Net;
using System.Dynamic;
using Newtonsoft.Json.Linq;

namespace SlackApi.Clients
{
    public class SlackWebClient : ISlackWebClient
    {
        #region Fields. 
        private static HttpClient client = new HttpClient();
        // [TeamId:MethodName][{current time, target time}]
        private static ConcurrentDictionary<string, KeyValuePair<Stopwatch, int>> blockedMethods;
        #endregion

        #region Properties
        public SlackUser User { get; }
        #endregion

        #region Constructor
        internal SlackWebClient() 
        { 
            blockedMethods = new ConcurrentDictionary<string, KeyValuePair<Stopwatch, int>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlackWebClient" /> class.
        /// </summary>
        public SlackWebClient(SlackUser user) : this()
        {
            User = user;
        }
        #endregion

        #region ISlackWebClient Interface.
        /// <summary>
        /// Calls a web api method.
        /// </summary>
        /// <param name="method">The method to call.</param>
        public async Task<T> CallApiMethod<T>(Methods.Method method, bool retryOnFailure = true) where T: Response
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
                
                blockedMethods.TryRemove(blockKey, out var dic);
            }
        
            var parameters = new Dictionary<string, string>();

            if (!String.IsNullOrEmpty(user.Token))
            {
                parameters["token"] = user.Token;
            }

            foreach (var parameter in method.Parameters)
            {
                if (parameter.Key == null || parameter.Value == null)
                {
                    continue;
                }
                parameters[parameter.Key] = parameter.Value;
            }

            var result = await "https://slack.com/api"
                .AllowHttpStatus("429")
                .AppendPathSegment(methodName)
                .PostAsync(new FormUrlEncodedContent(parameters)); 

            if ((int)result.StatusCode == 429 && retryOnFailure)
            {
                var retryAfter = result.Headers.FirstOrDefault(x => x.Key == "Retry-After");
                if (!int.TryParse(retryAfter.Value.ToString(), out var retrySeconds))
                {
                    retrySeconds = 60;
                }

                blockedMethods[blockKey] = 
                    new KeyValuePair<Stopwatch, int>(Stopwatch.StartNew(), retrySeconds);
                
                return await CallApiMethod<T>(method);
            }
                
            var resultString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(resultString);
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