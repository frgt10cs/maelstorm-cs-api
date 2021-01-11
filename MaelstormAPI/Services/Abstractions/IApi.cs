using System.Net.Http;
using System.Threading.Tasks;
using MaelstormApi.Models;

namespace MaelstormApi.Services.Abstractions
{
    public interface IApi
    {
        public bool IsAuthenticated { get; }

        /// <summary>
        /// Sends a request to the server
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data">Data which will be included into request</param>        
        /// <returns></returns>
        public Task<ServerResponse> RequestAsync(HttpRequestMessage message, object data = null);

        /// <summary>
        /// Sends a request to the server including authentication tokens
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data">Data which will be included into request</param>
        /// <typeparam name="T">Expecting type of returning server data</typeparam>
        /// <returns></returns>
        public Task<ServerResponse> AuthRequestAsync(HttpRequestMessage message, object data = null);

        Task<bool> EstablishSignalRConnection();
        public Task<bool> AuthenticateAsync(string login, string password);

        public Task LogoutAsync();
    }
}