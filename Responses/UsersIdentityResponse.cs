using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;
using SlackApi.Models;

namespace SlackApi.Responses
{
    public class UsersIdentityResponse : Response
    {
        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("team")]
        public Team Team { get; set; }
    }
}
