using System;
using System.Threading.Tasks;
using SlackApi;
using SlackApi.Clients;
using SlackApi.Events;
using SlackApi.Methods;
using SlackApi.Responses;

namespace BindRTMEvents
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var oauthAccessMethod = new OauthAccessMethod("Slack app client ID", "Slack app client secret", "temp auth code")
            {
                RedirectUri = "Slack app authorized redirect URL"
            };

            var webClient = await SlackWebClientFactory.CreateWebClient("userId", oauthAccessMethod);
            var connectionResponse = await webClient.CallApiMethod<ConnectResponse>(new RtmConnectMethod());
            var slackClient = new SlackRtmClient();

            if (connectionResponse.Ok)
            {
                slackClient.Connect("test", connectionResponse.Url);
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
