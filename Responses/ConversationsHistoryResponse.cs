using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;
using SlackApi.Models;

namespace SlackApi.Responses
{
    public class ConversationsHistoryResponse : Response
    {
        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("pin_count")]
        public long PinCount { get; set; }

        [JsonProperty("response_metadata")]
        public ResponseMetadata ResponseMetadata { get; set; }
    }
}
