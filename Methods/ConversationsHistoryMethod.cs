using System.Collections.Generic;

namespace SlackApi.Methods
{
    public class ConversationsHistoryMethod : Method
    {
        public ConversationsHistoryMethod(string channel)
        {
            Parameters.Add(new KeyValuePair<string, string>("channel", channel));
        }
    }
}