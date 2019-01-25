using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SlackApi.Events;
using SlackApi.Methods;
using SlackApi.Responses;

namespace SlackApi.Clients
{
    public interface ISlackWebClient
    {
        /// <summary>
        /// Calls a web api method.
        /// </summary>
        /// <param name="method">The method to invoke.</param>
        Task<T> CallApiMethod<T>(Method method, bool retryOnFailure = true) where T: Response;
    }
}
