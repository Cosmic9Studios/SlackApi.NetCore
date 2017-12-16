using System.Collections.Generic;
using Newtonsoft.Json;
using SlackApi.Models;

namespace SlackApi.Responses
{
    public class OauthAccessResponse : Response
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set;}

        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}