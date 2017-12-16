using Newtonsoft.Json;

namespace SlackApi.Models
{
    public class ResponseMetadata
    {
        [JsonProperty("next_cursor")]
        public string NextCursor { get; set; }
    }
}