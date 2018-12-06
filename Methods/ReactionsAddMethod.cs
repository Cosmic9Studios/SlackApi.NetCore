using System.Collections.Generic;

namespace SlackApi.Methods
{
    public class ReactionsAddMethod : Method
    {
        /// <summary>
        /// Initializes a new instance of the <cref="ReactionsAddMethod" /> class.
        /// </summary>
        /// <param name="reactionName">The reaction (emoji) name.</param>
        public ReactionsAddMethod(string reactionName)
            : base (RateLimit.Tier3)
        {
            Parameters.Add(new KeyValuePair<string, string>("name", reactionName));
        }

        public string Channel 
        {
            set
            {
                Parameters.Add(new KeyValuePair<string, string>("channel", value));
            }
        }

        public string File 
        {
            set
            {
                Parameters.Add(new KeyValuePair<string, string>("file", value));
            }
        }

        public string FileComment 
        { 
            set
            {   
                Parameters.Add(new KeyValuePair<string, string>("file_comment", value));
            }
        }

        public string Ts 
        {
            set
            {
                Parameters.Add(new KeyValuePair<string, string>("timestamp", value));
            }
        }
    }
}