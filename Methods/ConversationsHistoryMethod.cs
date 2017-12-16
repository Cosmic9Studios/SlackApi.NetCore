using System.Collections.Generic;

namespace SlackApi.Methods
{
    public class ConversationsHistoryMethod : IMethod
    {
        public ConversationsHistoryMethod(string channel)
        {
            Parameters = new List<KeyValuePair<string, string>>();
            Parameters.Add(new KeyValuePair<string, string>("channel", channel));
        }

        public List<KeyValuePair<string, string>> Parameters { get; }
    }
}