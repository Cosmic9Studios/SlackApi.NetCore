using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;
using SlackApi.Models;

namespace SlackApi.Responses
{
    public class TeamInfoResponse : Response
    {
        [JsonProperty("team")]
        public Team Team { get; set; }
    }
}
