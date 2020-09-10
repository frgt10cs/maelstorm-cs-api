using System.Net.Http;
using System.Threading.Tasks;
using MaelstormApi.Models;
using MaelstormDTO.Requests;
using MaelstormDTO.Responses;

namespace MaelstormApi
{
    public static class Account
    {
        public static async Task<ServerResponse> RegistrationAsync(RegistrationRequest registrationRequest)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "account/registration");
            var result = await Client.RequestAsync(message, registrationRequest);
            return result;
        }
    }
}