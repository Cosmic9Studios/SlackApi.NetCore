using Newtonsoft.Json;

namespace SlackApi.Events
{
    public class Event
    {
        [JsonProperty("type")]
        public string Type;
    }
}