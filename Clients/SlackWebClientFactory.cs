
using System.Threading.Tasks;
using SlackApi.Methods;
using SlackApi.Models;
using SlackApi.Responses;

namespace SlackApi.Clients
{
    public static class SlackWebClientFactory
    {
        public static async Task<SlackWebClient> CreateWebClient(string userId, OauthAccessMethod method)
        {
            var response = await new SlackWebClient().CallApiMethod<OauthAccessResponse>(method);
            if (response.Ok)
            {
                return new SlackWebClient(new SlackUser(response.AccessToken, userId, response.TeamId));
            }

            return null;
        }
    }
}