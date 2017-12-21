using System.Collections.Generic;

namespace SlackApi.Methods
{
    public class ConversationsInfoMethod : Method
    {
        public ConversationsInfoMethod(string channel)
        {
            Parameters.Add(new KeyValuePair<string, string>("channel", channel));
        }
    }
}