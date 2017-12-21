using Newtonsoft.Json;

namespace SlackApi.Models
{
    public class Subscription
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("thread_ts")]
        public string ThreadTs { get; set; }

        [JsonProperty("date_create")]
        public string DateCreate { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("last_read")]
        public string LastRead { get; set; }

        [JsonProperty("unread_count")]
        public long UnreadCount { get; set; }
    }
}