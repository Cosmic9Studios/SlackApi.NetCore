using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackApi.Methods
{
    public class OauthAccessMethod : Method
    {
        public OauthAccessMethod(string clientId, string clientSecret, string code)
        {
            Parameters.Add(new KeyValuePair<string, string>("client_id", clientId));
            Parameters.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
            Parameters.Add(new KeyValuePair<string, string>("code", code));
        }

        [JsonProperty("redirect_uri")]
        public string RedirectUri 
        {
            set
            {
                Parameters.Add(new KeyValuePair<string, string>("redirect_uri", value));
            }
        }
    }
}