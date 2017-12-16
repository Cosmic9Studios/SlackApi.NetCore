using System.Collections.Generic;

namespace SlackApi.Methods
{
    public interface IMethod
    {
        List<KeyValuePair<string, string>> Parameters { get; }
    }
}