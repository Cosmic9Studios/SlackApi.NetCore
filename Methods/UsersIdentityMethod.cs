namespace SlackApi.Methods
{
    public class UsersIdentityMethod : Method 
    { 
        /// <summary>
        /// Initializes a new instance of the <cref="UsersIdentityMethod" /> class.
        /// </summary>
        public UsersIdentityMethod()
            : base (RateLimit.Tier3) { }
    }
}