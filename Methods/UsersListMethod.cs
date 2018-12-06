namespace SlackApi.Methods
{
    public class UsersListMethod : Method 
    { 
        /// <summary>
        /// Initializes a new instance of the <cref="UsersListMethod" /> class.
        /// </summary>
        public UsersListMethod()
            : base (RateLimit.Tier2) { }
    }
}