using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;
using SlackApi.Models;

namespace SlackApi.Events
{
    public class ThreadMarkedEvent : Event
    {
        [JsonProperty("subscription")]
        public Subscription Subscription { get; set; }

        [JsonProperty("event_ts")]
        public string EventTs { get; set; }
    }
}
