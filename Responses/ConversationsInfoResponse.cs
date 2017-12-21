using System.Collections.Generic;
using Newtonsoft.Json;
using SlackApi.Models;

namespace SlackApi.Responses
{
    public class ConversationsInfoResponse : Response
    {
        [JsonProperty("channel")]
        public Channel Channel { get; set; }
    }
}