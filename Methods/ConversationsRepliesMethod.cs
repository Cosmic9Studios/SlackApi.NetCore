using System.Collections.Generic;

namespace SlackApi.Methods
{
    public class ConversationsRepliesMethod : Method
    {
        /// <summary>
        /// Initializes a new instance of the <cref="ConversationsRepliesMethod" /> class.
        /// </summary>
        /// <param name="channel">The conversation ID to fetch thread from.</param>
        /// <param name="ts">The unique identifier of a thread's parent message.</param>
        public ConversationsRepliesMethod(string channel, string ts)
            : base (RateLimit.Tier3)
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