using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MaelstormDTO.Requests;
using MaelstormDTO.Responses;

namespace maelstorm_api
{
    public static class Sessions
    {
        public static async Task<List<ClientSessions>> GetSessionsAsync(int offset = 0, int count = 10)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"sessions?offset={offset}&count={count}");
            var result = await Client.AuthRequestAsync<List<ClientSessions>>(message);
            return result;
        }

        public static async Task<ClientSessions> GetSessionAsync(string sessionId)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"sessions/{sessionId}");
            var result = await Client.AuthRequestAsync<ClientSessions>(message);
            return result;
        }

        public static async Task CloseAsync(string sessionId, bool banDevice)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, $"sessions/close/{sessionId}");
            await Client.AuthRequestAsync<object>(message, new CloseSessionRequest()
            {
                SessionId = sessionId,
                BanDevice = banDevice
            });
        }

        public static async Task<List<OnlineStatus>> GetOnlineStatuses(int[] usersIds)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"online-statuses/{usersIds}");
            var result = await Client.AuthRequestAsync<List<OnlineStatus>>(message);
            return result;
        }
    }
}