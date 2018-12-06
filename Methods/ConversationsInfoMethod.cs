using System.Collections.Generic;

namespace SlackApi.Methods
{
    public class ConversationsInfoMethod : Method
    {
        public ConversationsInfoMethod(string channel)
            : base (RateLimit.Tier3)
        {
            Parameters.Add(new KeyValuePair<string, string>("channel", channel));
        }
    }
}