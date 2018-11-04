using System;
using System.Threading.Tasks;
using SlackApi;
using SlackApi.Methods;
using SlackApi.Responses;

namespace CallAPIMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            var tempAuthCode = "Temporary authorization code given by Slack when a user authorizes your app";
            var oauthAccessMethod = new OauthAccessMethod("Slack app client ID", "Slack app client secret", tempAuthCode)
            {
                RedirectUri = "Slack app authorized redirect URL"
            };
            var slackClient = await AuthorizeClient(oauthAccessMethod);

            Task.Run(() => TestAuth(slackClient));

            Console.Read();
        }

        /// <summary>
        /// Exchange the temporary code for the user's secret token.
        /// </summary>
        /// <param name="oauthAccessMethod">OAuth access method options.</param>
        /// <returns>Client that will allow taking authorized actions on behalf of the user.</returns>
        private static async Task<SlackClient> AuthorizeClient(OauthAccessMethod oauthAccessMethod)
        {
            var tempClient = new SlackClient("");
            var oauthResponse = await tempClient.CallApiMethod<OauthAccessResponse>(oauthAccessMethod);
            
            return new SlackClient(oauthResponse.AccessToken);
        }

        /// <summary>
        /// Check the client's basic authentication and identity information.
        /// </summary>
        private static async Task TestAuth(ISlackClient slackClient)
        {
            var authTestResponse = await slackClient.CallApiMethod<AuthTestReponse>(new AuthTestMethod());

            if (authTestResponse.Ok)
            {
                Console.WriteLine($"{ authTestResponse.User } has been authorized.");
            }
            else
            {
                Console.WriteLine($"Failed to authorize user - { authTestResponse.Error }");
            }
        }
    }
}
