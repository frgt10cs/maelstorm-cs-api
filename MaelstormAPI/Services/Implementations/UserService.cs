using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MaelstormApi.Services.Abstractions;
using MaelstormApi.Services.Implementations;
using MaelstormDTO.Responses;

namespace MaelstormAPI.Services.Implementations
{
    public class UserService:IUserService
    {
        public IApi _api;

        public UserService(IApi api)
        {
            this._api = api;
        }
        public  async Task<List<UserInfo>> FindUsersByNicknameAsync(string nickname)
        {
            var message = new HttpRequestMessage(HttpMethod.Get,$"users?query={nickname}");
            var response = await _api.AuthRequestAsync(message);
            return response.GetContent<List<UserInfo>>();
        }
    }
}