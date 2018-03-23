using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;
using SlackApi.Models;

namespace SlackApi.Responses
{
    public class UsersListResponse : Response
    {
        [JsonProperty("members")]
        public List<User> Users { get; set; }
    }
}
