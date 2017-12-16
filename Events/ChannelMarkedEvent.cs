using Newtonsoft.Json;

namespace SlackApi.Events
{
    public class ChannelMarkedEvent : Event
    {
        [JsonProperty("channel")]
        public string Channel;

        [JsonProperty("ts")]
        public string Ts;
    }
}