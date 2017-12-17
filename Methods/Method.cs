using System.Collections.Generic;

namespace SlackApi.Methods
{
    public class Method
    {
        public Method()
        {
            Parameters = new List<KeyValuePair<string, string>>();
        }

        public List<KeyValuePair<string, string>> Parameters { get; }
    }
}