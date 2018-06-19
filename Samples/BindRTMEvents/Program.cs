using System;
using System.Threading.Tasks;
using SlackApi;
using SlackApi.Events;

namespace BindRTMEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            var slackClient = new SlackClient("Slack User Token");
            var connectionResponse = Task.Run(() => slackClient.Connect()).Result;

            if (connectionResponse.Ok)
            {
                slackClient.BindEvent<ReactionAddedEvent>(ReactionAddedCallback);
                slackClient.BindEvent<ReactionRemovedEvent>(ReactionRemovedCallback);

                Console.WriteLine("User connected");
            }

            Console.Read();
        }

        private static void ReactionAddedCallback(ReactionAddedEvent reactionAddedData)
        {
            Console.WriteLine($"{ reactionAddedData.Reaction } added by { reactionAddedData.User }");
        }

        private static void ReactionRemovedCallback(ReactionRemovedEvent reactionRemovedData)
        {
            Console.WriteLine($"{ reactionRemovedData.Reaction } removed by { reactionRemovedData.User }");
        }
    }
}
