using System.Collections.Generic;

namespace SlackApi.Methods
{
    public class ConversationsRepliesMethod : Method
    {
        public ConversationsRepliesMethod(string channel, string ts)
        {
            Parameters.Add(new KeyValuePair<string, string>("channel", channel));
            Parameters.Add(new KeyValuePair<string, string>("ts", ts));
        }

        public int Limit
        {
            set
            {
                Parameters.Add(new KeyValuePair<string, string>("limit", value.ToString()));
            }
        }

        public string Latest
        {
            set
            {
                Parameters.Add(new KeyValuePair<string, string>("latest", value));
            }
        }

        public bool Inclusive
        {
            set
            {
                Parameters.Add(new KeyValuePair<string, string>("inclusive", value.ToString()));
            }
        }
    }
}