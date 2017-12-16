using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackApi.Methods
{
    public class UsersInfoMethod : IMethod
    {
        public UsersInfoMethod(string userId)
        {
            Parameters.Add(new KeyValuePair<string, string>("user", userId));
        }

        public List<KeyValuePair<string, string>> Parameters => new List<KeyValuePair<string, string>>();
    }
}