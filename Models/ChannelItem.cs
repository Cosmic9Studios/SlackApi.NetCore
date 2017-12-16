using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace SlackApi.Models
{
    public class ChannelItem
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("ts")]
        public string Ts { get; set; }

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("file_comment")]
        public string FileComment { get; set; }
    }
}