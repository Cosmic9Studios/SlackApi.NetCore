using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackApi.Methods
{
    public class OauthAccessMethod : IMethod
    {
        public OauthAccessMethod(string clientId, string clientSecret, string code)
        {
            Parameters.Add(new KeyValuePair<string, string>("client_id", clientId));
            Parameters.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
            Parameters.Add(new KeyValuePair<string, string>("code", code));
        }

        public List<KeyValuePair<string, string>> Parameters => new List<KeyValuePair<string, string>>();

        [JsonProperty("redirect_url")]
        public string RedirectUrl { get; set; }
    }
}