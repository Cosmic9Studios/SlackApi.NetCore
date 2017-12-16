using Newtonsoft.Json;

namespace SlackApi.Responses
{
    public class Response
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("warning")]
        public string Warning;

        [JsonProperty("error")]
        public string Error;
    }
}