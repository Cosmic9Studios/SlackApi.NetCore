using System.Collections.Generic;

namespace SlackApi.Methods
{
    public class UsersInfoMethod : Method
    {
        /// <summary>
        /// Initializes a new instance of the <cref="UsersInfoMethod" /> class.
        /// </summary>
        public UsersInfoMethod(string userId)
            : base (RateLimit.Tier4)
        {
            Parameters.Add(new KeyValuePair<string, string>("user", userId));
        }
    }
}