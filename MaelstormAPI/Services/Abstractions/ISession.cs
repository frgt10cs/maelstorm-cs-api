using System.Collections.Generic;
using System.Threading.Tasks;
using MaelstormDTO.Responses;

namespace MaelstormApi.Services.Abstractions
{
    public interface ISessionService
    {
        public Task<List<UserSessions>> GetSessionsAsync(int offset = 0, int count = 10);
        
        public Task<UserSessions> GetSessionAsync(string sessionId);

        public Task CloseAsync(string sessionId, bool banDevice);

        public Task<List<OnlineStatus>> GetOnlineStatuses(int[] usersIds);

    }
}