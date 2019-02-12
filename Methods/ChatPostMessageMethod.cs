using System.Collections.Generic;

namespace SlackApi.Methods
{
    public class ChatPostMessageMethod : Method
    {
        public ChatPostMessageMethod(string channel, string text) : 
            base (RateLimit.Special)
        {
            Parameters.Add(new KeyValuePair<string, string>("channel", channel));
            Parameters.Add(new KeyValuePair<string, string>("text", text));
        }

        public bool AsUser 
        {
            set
            {
                Parameters.Add(new KeyValuePair<string, string>("as_user", value.ToString()));
            }
        }
    }
}