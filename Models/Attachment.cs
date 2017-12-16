using Newtonsoft.Json;

namespace SlackApi.Models
{
    public class Attachment
    {
        [JsonProperty("service_name")]
        public string ServiceName { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("fallback")]
        public string Fallback { get; set; }

        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }

        [JsonProperty("thumb_width")]
        public long ThumbWidth { get; set; }

        [JsonProperty("thumb_height")]
        public long ThumbHeight { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }
}