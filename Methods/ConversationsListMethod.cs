using System.Collections.Generic;

namespace SlackApi.Methods
{
    public class ConversationsListMethod : IMethod 
    { 
        public List<KeyValuePair<string, string>> Parameters => new List<KeyValuePair<string, string>>();
    }
}