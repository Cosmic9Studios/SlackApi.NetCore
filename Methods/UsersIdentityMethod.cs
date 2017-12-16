using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackApi.Methods
{
    public class UsersIdentityMethod : IMethod
    {
        public List<KeyValuePair<string, string>> Parameters => new List<KeyValuePair<string, string>>();
    }
}