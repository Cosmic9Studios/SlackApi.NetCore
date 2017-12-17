using System.Collections.Generic;

namespace SlackApi.Methods
{
    public class UsersInfoMethod : Method
    {
        public UsersInfoMethod(string userId)
        {
            Parameters.Add(new KeyValuePair<string, string>("user", userId));
        }
    }
}