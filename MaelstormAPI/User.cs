using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MaelstormDTO.Responses;

namespace MaelstormApi
{
    public static class User
    {
        public static async Task<List<UserInfo>> FindUsersByNicknameAsync(string nickname)
        {
            var message = new HttpRequestMessage(HttpMethod.Get,$"users?query={nickname}");
            var response = await Client.AuthRequestAsync(message);
            return response.GetContent<List<UserInfo>>();
        }
    }
}