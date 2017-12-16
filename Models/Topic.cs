using Newtonsoft.Json;

namespace SlackApi.Models
{
    public class Topic
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        [JsonProperty("last_set")]
        public long LastSet { get; set; }
    }
}