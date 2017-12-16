using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;
using SlackApi.Models;

namespace SlackApi.Responses
{
    public class UsersInfoResponse : Response
    {
        [JsonProperty("user")]
        public User User { get; set; }
    }
}
