using System.Security;

namespace SlackApi.Models
{
    public class SlackUser
    {
        /// <summary>
        /// Initializes an instance ode the <cref="SlackUser" /> class.
        /// </summary>
        /// <param name="token">The users token.</param>
        /// <param name="id">The users id.</param>
        /// <param name="teamId">The users team id.</param>
        public SlackUser(string token, string id, string teamId)
        {
            Token = token;
            Id = id;
            TeamId = teamId;
        }

        /// <summary>
        /// Gets the token for the user.
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Gets the users id.
        /// </summary>
        public string Id { get; } 

        /// <summary>
        /// Gets the users team id.
        /// </summary>
        public string TeamId { get; set; }
    }
}