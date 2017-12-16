using System.Collections.Generic;
using Newtonsoft.Json;
using SlackApi.Models;

namespace SlackApi.Responses
{
    public class ConversationsListResponse : Response
    {
        [JsonProperty("channels")]
        public List<Channel> Channels { get; set; }
    }
}