using System.Collections.Generic;

namespace SlackApi.Methods
{
    public class ConversationsListMethod : Method 
    { 
        public string Types
        {
            set
            {
                Parameters.Add(new KeyValuePair<string, string>("types", value));
            }
        }
    }
}