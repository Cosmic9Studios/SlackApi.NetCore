namespace SlackApi.Methods
{
    public class TeamInfoMethod : Method 
    { 
        /// <summary>
        /// Initializes a new instance of the <cref="TeamInfoMethod" /> class.
        /// </summary>
        public TeamInfoMethod()
            : base (RateLimit.Tier3) { }
    }
}