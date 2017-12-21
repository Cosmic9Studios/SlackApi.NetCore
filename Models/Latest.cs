using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace SlackApi.Models
{
    public class Latest
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("ts")]
        public string Ts { get; set; }
    }
}