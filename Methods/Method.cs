using System.Collections.Generic;

namespace SlackApi.Methods
{
    public class Method
    {
        /// <summary>
        /// Initializes a new instance of the <cref="Method" /> class.
        /// </summary>
        /// <param name="tier">The rate limiting tier the method belongs to.</param>
        public Method(RateLimit tier)
        {
            Parameters = new List<KeyValuePair<string, string>>();
            Tier = tier;
        }

        /// <summary>
        /// Gets the parameters passed to the method.
        /// </summary>
        public List<KeyValuePair<string, string>> Parameters { get; }

        /// <summary>
        /// Gets the rate limiting tier the method belongs to.
        /// </summary>
        public RateLimit Tier { get; }
    }
}