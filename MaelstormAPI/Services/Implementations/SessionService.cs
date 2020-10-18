using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MaelstormApi.Services.Abstractions;
using MaelstormDTO.Requests;
using MaelstormDTO.Responses;

namespace MaelstormApi.Services.Implementations
{
    public class SessionService : ISessionService
    {
        private IApi _api;
        public SessionService(IApi api)
        {
            this._api = api;
        }
        
        public async Task<List<UserSessions>> GetSessionsAsync(int offset = 0, int count = 10)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"sessions?offset={offset}&count={count}");
            var response = await _api.AuthRequestAsync(message);
            if (response.Ok)
            {
                var sessions = response.GetContent<List<UserSessions>>();
                return sessions;
            }
            return new List<UserSessions>();
        }

        public async Task<UserSessions> GetSessionAsync(string sessionId)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"sessions/{sessionId}");
            var response = await _api.AuthRequestAsync(message);
            if (response.Ok)
            {
                return response.GetContent<UserSessions>();
            }
            return null;
        }

        public async Task CloseAsync(string sessionId, bool banDevice)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"sessions/close/{sessionId}");
            await _api.AuthRequestAsync(message, new CloseSessionRequest()
            {
                SessionId = sessionId,
                BanDevice = banDevice
            });
        }

        public async Task<List<OnlineStatus>> GetOnlineStatuses(int[] usersIds)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"online-statuses/{usersIds}");
            var response = await _api.AuthRequestAsync(message);
            if (response.Ok)
            {
                var onlineStatuses = response.GetContent<List<OnlineStatus>>();
                return onlineStatuses;
            }
            return new List<OnlineStatus>();
        }
    }
}