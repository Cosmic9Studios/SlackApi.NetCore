namespace SlackApi.Methods
{
    public class RtmConnectMethod : Method 
    { 
        /// <summary>
        /// Initializes a new instance of the <cref="RtmConnectMethod" /> class.
        /// </summary>
        public RtmConnectMethod()
            : base (RateLimit.Tier1) { }
    }
}