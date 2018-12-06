using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackApi.Methods
{
    public class OauthAccessMethod : Method
    {
        /// <summary>
        /// Initializes a new instance of the <cref="OauthAccessMethod" /> class.
        /// </summary>
        /// <param name="clientId">The client id issued when you created your application.</param>
        /// <param name="clientSecret">The client secret issued when you created your application.</param>
        /// <param name="code">The code param returned via the OAuth callback.</param>
        public OauthAccessMethod(string clientId, string clientSecret, string code) : 
            base (RateLimit.Tier4)
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