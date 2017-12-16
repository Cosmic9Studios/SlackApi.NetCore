using Newtonsoft.Json;
using SlackApi.Models;

namespace SlackApi.Events
{
    public class ReactionAddedEvent : Event
    {
        [JsonProperty("user")]
        public string User;

        [JsonProperty("reaction")]
        public string Reaction;

        [JsonProperty("item_user")]
        public string ItemUser;

        [JsonProperty("event_ts")]
        public string EventTs { get; set; }

        [JsonProperty("item")]
        public ChannelItem Item { get; set; }
    }
}