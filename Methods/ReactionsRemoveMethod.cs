using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackApi.Methods
{
    public class ReactionsRemoveMethod : ReactionsAddMethod
    {
        public ReactionsRemoveMethod(string reactionName) : base (reactionName) { }
    }
}