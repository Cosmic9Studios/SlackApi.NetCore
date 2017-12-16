    using Newtonsoft.Json;

namespace SlackApi.Models
{
    public class Icon
    {
        [JsonProperty("image_34")]
        public string Image34 { get; set; }

        [JsonProperty("image_44")]
        public string Image44 { get; set; }

        [JsonProperty("image_68")]
        public string Image68 { get; set; }

        [JsonProperty("image_88")]
        public string Image88 { get; set; }

        [JsonProperty("image_102")]
        public string Image102 { get; set; }

        [JsonProperty("image_132")]
        public string Image132 { get; set; }

        [JsonProperty("image_default")]
        public bool ImageDefault { get; set; }
    }
}