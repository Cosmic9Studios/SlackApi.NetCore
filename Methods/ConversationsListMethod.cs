using System.Collections.Generic;

namespace SlackApi.Methods
{
    public class ConversationsListMethod : Method 
    { 
        /// <summary>
        /// Initializes a new instance of the <cref="ConversationsListMethod" /> class.
        /// </summary>
        public ConversationsListMethod() 
            : base (RateLimit.Tier2) { }

        public string Types
        {
            set
            {
                Parameters.Add(new KeyValuePair<string, string>("types", value)); // limit 100 
            }
        }
    }
}