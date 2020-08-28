using System.Net.Http;
using System.Threading.Tasks;
using MaelstormDTO.Requests;
using MaelstormDTO.Responses;

namespace maelstorm_api
{
    public static class Account
    {
        public static async Task<RequestResult<object>> RegistrationAsync(RegistrationRequest registrationRequest)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "account/registration");
            var result = await Client.RequestAsync<object>(message, registrationRequest);
            return result;
        }
    }
}