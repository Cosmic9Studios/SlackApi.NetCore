using Newtonsoft.Json;

namespace SlackApi.Models
{
    public class Reply
    {
        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("ts")]
        public string Ts { get; set; }
    }
}