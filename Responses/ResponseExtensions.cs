using System;

namespace SlackApi.Responses
{
    public static class ResponseExtensions
    {
        public static void DisplayErrors(this Response response, Action<string> output)
        {
            if (!response.Ok)
            {
                if (!string.IsNullOrWhiteSpace(response.Warning))
                {
                    output($"Warning: {response.Warning}");
                }

                if (!string.IsNullOrWhiteSpace(response.Error))
                {
                    output($"Error: {response.Error}");
                }
            }
        }
    }
}