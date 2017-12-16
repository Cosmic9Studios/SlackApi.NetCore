using Newtonsoft.Json;

namespace SlackApi.Responses
{
    public class ConnectResponse : Response
    {
        [JsonProperty("url")]
        public string Url;
    }
}