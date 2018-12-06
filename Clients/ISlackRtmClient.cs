using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SlackApi.Events;
using SlackApi.Methods;
using SlackApi.Responses;

namespace SlackApi.Clients
{
    public interface ISlackRtmClient
    {
        /// <summary>
        /// Connects to the slack api.
        /// </summary>
        void Connect(string socketUrl);

        /// <summary>
        /// Disconnects from the slack api.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Binds a callback to an event.
        /// </summary>
        /// <param name="callback">The callback to bind.</param>
        void BindEvent<T>(Action<T> callback) where T : Event;

        /// <summary>
        /// Unbinds a bound event.
        /// </summary>
        /// <param name="callback">The callback to unbind.</param>
        void UnbindEvent<T>() where T : Event;
    }
}
