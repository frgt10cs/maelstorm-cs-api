using System.Net.Http;
using System.Threading.Tasks;
using MaelstormApi.Models;
using MaelstormApi.Services.Abstractions;
using MaelstormApi.Services.Implementations;
using MaelstormDTO.Requests;

namespace MaelstormAPI.Services.Implementations
{
    public class AccountService:IAccountService
    {
        private Api _api;
        public AccountService(Api api)
        {
            this._api = api;
        }
        public async Task<ServerResponse> RegistrationAsync(RegistrationRequest registrationRequest)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "account/registration");
            var result = await _api.RequestAsync(message, registrationRequest);
            return result;
        }
    }
}