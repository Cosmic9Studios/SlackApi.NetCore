using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackApi.Methods
{
    public class ReactionsAddMethod : IMethod
    {
        public ReactionsAddMethod(string reactionName)
        {
            Parameters.Add(new KeyValuePair<string, string>("name", reactionName));
        }

        public List<KeyValuePair<string, string>> Parameters => new List<KeyValuePair<string, string>>();

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("file_comment")]
        public string FileComment { get; set; }

        [JsonProperty("timestamp")]
        public string Ts { get; set; }
    }
}