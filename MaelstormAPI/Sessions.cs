using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MaelstormDTO.Requests;
using MaelstormDTO.Responses;

namespace MaelstormApi
{
    public static class Sessions
    {
        public static async Task<List<UserSessions>> GetSessionsAsync(int offset = 0, int count = 10)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"sessions?offset={offset}&count={count}");
            var response = await Client.AuthRequestAsync(message);
            if (response.Ok)
            {
                var sessions = response.GetContent<List<UserSessions>>();
                return sessions;
            }
            return new List<UserSessions>();
        }

        public static async Task<UserSessions> GetSessionAsync(string sessionId)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"sessions/{sessionId}");
            var response = await Client.AuthRequestAsync(message);
            if (response.Ok)
            {
                return response.GetContent<UserSessions>();
            }
            return null;
        }

        public static async Task CloseAsync(string sessionId, bool banDevice)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, $"sessions/close/{sessionId}");
            await Client.AuthRequestAsync(message, new CloseSessionRequest()
            {
                SessionId = sessionId,
                BanDevice = banDevice
            });
        }

        public static async Task<List<OnlineStatus>> GetOnlineStatuses(int[] usersIds)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"online-statuses/{usersIds}");
            var response = await Client.AuthRequestAsync(message);
            if (response.Ok)
            {
                var onlineStatuses = response.GetContent<List<OnlineStatus>>();
                return onlineStatuses;
            }
            return new List<OnlineStatus>();
        }
    }
}