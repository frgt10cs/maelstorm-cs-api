using System.Collections.Generic;
using System.Threading.Tasks;
using MaelstormDTO.Responses;

namespace MaelstormApi.Services.Abstractions
{
    public interface IUserService
    {
        public Task<List<UserInfo>> FindUsersByNicknameAsync(string nickname);
    }
}