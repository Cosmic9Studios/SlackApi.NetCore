using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SlackApi.Events;
using SlackApi.Methods;
using SlackApi.Responses;

namespace SlackApi
{
    public interface ISlackClient
    {
        /// <summary>
        /// Connects to the slack client.
        /// </summary>
        /// <param name="token">The token used to identify the user.</param>
        Task<ConnectResponse> Connect(string token);

        /// <summary>
        /// Calls a web api method.
        /// </summary>
        /// <param name="method">The method to invoke.</param>
        Task<T> CallApiMethod<T>(IMethod method) where T: Response;
        
        /// <summary>
        /// Binds a callback to an event.
        /// </summary>
        /// <param name="callback"></param>
        void BindEvent<T>(Action<T> callback) where T : Event;
    }
}
